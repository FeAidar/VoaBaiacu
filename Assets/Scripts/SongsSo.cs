using UnityEngine;
[CreateAssetMenu(fileName = "Songs Reference", menuName = "ScriptableObjects/Songs Reference", order = 1)]
public class SongsSo : ScriptableObject
{
   public AudioClip mainGameSong;
   public AudioClip menuSong;
   public AudioClip gameOverSong;
   public AudioClip creditsSong;
}
