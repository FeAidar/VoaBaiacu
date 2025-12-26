using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneReferences", menuName = "ScriptableObjects/Scene Library", order = 1)]
public class SceneReferences : ScriptableObject
{
    public LevelSO IntroScene;
    public  LevelSO GameScene;
    public  LevelSO CamerasScene;
    public  LevelSO GameOverScene;
    public  LevelSO CreditsScene;
    public  LevelSO[] StartingScenes;
}
