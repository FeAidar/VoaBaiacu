using UnityEngine;

public class InlineSOAttribute : PropertyAttribute
{
    public bool readOnly;

    public InlineSOAttribute(bool readOnly = false)
    {
        this.readOnly = readOnly;
    }
}