using UnityEngine;

[System.Serializable]
    public struct Score : IValidatable
    {
        [Range(-1000, 1000)] public int score;

        public void Validate()
        {
            score = Mathf.RoundToInt( score / 50f) * 50;
        }
    }

    public interface IValidatable
    {
        void Validate();
    }

