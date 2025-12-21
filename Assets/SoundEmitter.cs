using UnityEngine;



    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] protected AudioSource audioSource;
        public void PlayAudio(AudioClip[] clip)
        {
            if (clip == null) return;
            if(clip.Length == 0) return;
            audioSource.PlayOneShot(clip[Random.Range(0, clip.Length)]);
        }
    }
