using System;

public static class ScoreEvents
{
    public static Action<int> OnScoreEvent;
    public static Action<int> OnTotalScoreEvent;
    public static Action<int> OnEndGameEvent;

    public static void Raise(int value)
    {
        OnScoreEvent?.Invoke(value);
    }
    public static void FullScore(int value)
    {
       OnTotalScoreEvent?.Invoke(value);
    }

    public static void RegisterHighScore(int value)
    {
       HighScoreManager.TrySave(value);
    }
    
}