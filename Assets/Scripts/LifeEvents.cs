using System;
using UnityEngine.Networking.PlayerConnection;

public static class LifeEvents
{
    public static Action<int> OnLifeEvent;
    public static Action OnGameOverEvent;
    public static Action<int> OnStartGame;

    public static void LifeChanged(int value)
    {
        OnLifeEvent?.Invoke(value);
    }
    public static void GameOver()
    {
       OnGameOverEvent?.Invoke();
    }

    public static void StartGame(int value)
    {
        OnStartGame?.Invoke(value);
    }
}