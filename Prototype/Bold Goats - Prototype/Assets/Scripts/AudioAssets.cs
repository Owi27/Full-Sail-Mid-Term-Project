using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioAssets : MonoBehaviour
{
    public static AudioAssets instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        SoundManager.Initialize();
    }

    public SoundAudioClip[] soundAudioClipArray;
    public MusicAudioClip[] musicAudioClipArray;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundMixer;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    [System.Serializable]
    public class MusicAudioClip
    {
        public SoundManager.Music song;
        public AudioClip audioClip;
    }
}
