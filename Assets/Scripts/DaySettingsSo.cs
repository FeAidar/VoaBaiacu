
    using UnityEngine;
    [CreateAssetMenu(fileName = "Day Settings", menuName = "ScriptableObjects/DaySettings", order = 1)]
    public class DaySettingsSo : ScriptableObject
    {
        
        
            public TimePeriod period;
            public float duration;
            public Hazards[] hazards;
            public PowerUps[] powerUps;
        

    }
