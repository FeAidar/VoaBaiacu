using System;
using System.Reflection;

public static class RecursiveValidator
{
    public static object ValidateObject(object obj)
    {
        if (obj == null)
            return null;

        Type type = obj.GetType();

        // Se for Score (ou qualquer validável)
        if (obj is IValidatable validatable)
        {
            validatable.Validate();
            obj = validatable;
        }

        // Arrays (HazardScoreSystem[], WaterScoreSystem[], etc.)
        if (type.IsArray)
        {
            Array array = (Array)obj;
            for (int i = 0; i < array.Length; i++)
            {
                object element = array.GetValue(i);
                object validatedElement = ValidateObject(element);
                array.SetValue(validatedElement, i);
            }
            return array;
        }

        // Só entra em structs (evita UnityObjects, classes, etc.)
        if (!type.IsValueType || type.IsPrimitive)
            return obj;

        var fields = type.GetFields(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        );

        foreach (var field in fields)
        {
            object fieldValue = field.GetValue(obj);
            if (fieldValue == null)
                continue;

            object validatedValue = ValidateObject(fieldValue);

            
            field.SetValue(obj, validatedValue);
        }

        return obj;
    }
}