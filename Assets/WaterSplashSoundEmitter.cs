using UnityEngine;

    public class WaterSplashSoundEmitter : SoundEmitter
    {
        [SerializeField] private AudioClip[] playerWaterAudioClips;
        public AudioClip[] PlayerWaterAudioClips { get => playerWaterAudioClips; set => playerWaterAudioClips = value; }
        [SerializeField] private AudioClip[] otherWaterAudioClips;
        public AudioClip[] OtherWaterAudioClips { get => otherWaterAudioClips; set => otherWaterAudioClips = value; }
        [SerializeField] private AudioClip[] dangerWaterAudioClips;
        public AudioClip[] DangerWaterAudioClips { get => dangerWaterAudioClips; set => dangerWaterAudioClips = value; }

    }
