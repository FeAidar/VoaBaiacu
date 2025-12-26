using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SceneReference", menuName = "ScriptableObjects/Scene Reference", order = 1)]
public class LevelSO : ScriptableObject
{
   
    [Header("Scene Reference")]
#if UNITY_EDITOR
    [Tooltip("Usado apenas no Editor")]
    public SceneAsset sceneAsset;
#endif

    [SerializeField, HideInInspector]
    private string sceneName;

    public string SceneName => sceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset == null)
        {
            sceneName = string.Empty;
            return;
        }

        sceneName = sceneAsset.name;
    }
#endif
}