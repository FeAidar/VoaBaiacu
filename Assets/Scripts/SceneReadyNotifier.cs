using System;
using UnityEngine;

public class SceneReadyNotifier : MonoBehaviour
{
    public static event Action<string> OnSceneReady;
    public static bool IsSceneReady { get; private set; }
    public static string LastReadyScene { get; private set; }

    private void Start()
    {
        IsSceneReady = true;
        LastReadyScene = gameObject.scene.name;
        OnSceneReady?.Invoke(LastReadyScene);
    }
}