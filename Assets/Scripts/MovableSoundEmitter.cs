using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MovableSoundEmitter : SoundEmitter
{
    [SerializeField] private AudioClip[] appearAudioClips;
    public AudioClip[] AppearAudioClips { get => appearAudioClips; set => appearAudioClips = value; }
    [SerializeField] private AudioClip[] hitAudioClips;
    public AudioClip[] HitAudioClips { get => hitAudioClips; set => hitAudioClips = value; }
    [SerializeField] private AudioClip[] endAudioClips;
    public AudioClip[] EndAudioClips { get => endAudioClips; set => endAudioClips = value; }
    [SerializeField] private AudioClip[] waterAudioClips;
    public AudioClip[] WaterAudioClips { get => waterAudioClips; set => waterAudioClips = value; }
  

  
}
