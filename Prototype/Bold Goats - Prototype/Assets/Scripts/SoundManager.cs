using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{

    public enum Sound
    {
        PlayerRun,
        PlayerWalk,
        PlayerThrowDistractable,
        EnableXRay,
        LaserKillsPlayer,
        EnemyWalk,
        EnemyReturn,
        EnemyAttack,
        EnemySpotPlayer,
        EnemyChasePlayer,
        SpotlightFindsPlayer,
        StartGame,
        ClickMenuItem,
        HoverMenuItem,
        PauseGame,
        UnpauseGame,
        ActivateTerminal,
        PlayerWaterDeath,
        LaserIdle,
        EnterTreasureRoom,
        DoorOpen,
        WinGame,
        ApplicationQuit,
        DistractionExplosion
    }

    public enum Music
    {
        Menu,
        CentralRoom,
        Outdoor,
        Spotlight,
        LaserMaze,
        WinArea
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static List<AudioSource> musicObjects;

    private static GameObject oneShotGameObject;
    public static AudioSource oneShotAudioSource;

    public static AudioMixerGroup musicMixer;
    public static AudioMixerGroup soundMixer;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        musicObjects = new List<AudioSource>(6);
        soundTimerDictionary[Sound.PlayerRun] = 0f;
        soundTimerDictionary[Sound.PlayerWalk] = 0f;
        soundTimerDictionary[Sound.EnemyWalk] = 0f;
        soundTimerDictionary[Sound.EnemySpotPlayer] = 0f;
        soundTimerDictionary[Sound.EnemyChasePlayer] = 0f;
        soundTimerDictionary[Sound.PlayerWaterDeath] = 0f;
        musicMixer = AudioAssets.instance.musicMixer;
        soundMixer = AudioAssets.instance.soundMixer;
    }

    #region Sound FX
    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObject = new GameObject("Sound");
            Object.DontDestroyOnLoad(soundObject);
            soundObject.transform.position = position;
            AudioSource audio = soundObject.AddComponent<AudioSource>();
            audio.outputAudioMixerGroup = soundMixer;
            audio.clip = GetAudioClip(sound);
            audio.Play();
            Object.Destroy(soundObject, audio.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {

            if (oneShotGameObject == null)
            {

                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                Object.DontDestroyOnLoad(oneShotGameObject);
                oneShotAudioSource.outputAudioMixerGroup = soundMixer;
            } 
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    private static bool CanPlaySound(Sound sound)
    {
        
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerRun:
                return CanPlayLogic(sound, 1f);
            case Sound.PlayerWalk:
                return CanPlayLogic(sound, .5f);
            case Sound.EnemyWalk:
                return CanPlayLogic(sound, .4f);
            case Sound.EnemySpotPlayer:
                return CanPlayLogic(sound, 4f);
            case Sound.EnemyChasePlayer:
                return CanPlayLogic(sound, 6f);
            case Sound.PlayerWaterDeath:
                return CanPlayLogic(sound, 1f);
        }
    }
    private static bool CanPlayLogic(Sound sound, float maxTimer)
    {
        if (soundTimerDictionary.ContainsKey(sound))
        {
            float lastTimePlayed = soundTimerDictionary[sound];
            float playerMoveTimerMax = maxTimer;
            if (lastTimePlayed + playerMoveTimerMax < Time.time)
            {
                soundTimerDictionary[sound] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        
        return null;
    }
    #endregion

    #region Music
    public static void PlayMusic(Music song)
    {
        foreach (var obj in musicObjects)
        {
            if (obj.gameObject.name == song.ToString())
            {
                obj.Play();
                return;
            }
        }
        GameObject soundObject = new GameObject(song.ToString());
        soundObject.transform.parent = Camera.main.transform;
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = musicMixer;
        audio.clip = GetMusicClip(song);
        audio.Play();
        musicObjects.Add(audio);
    }
    public static void StopAllMusic()
    {
        foreach (var obj in musicObjects)
        {
            obj.Stop();
        }
    }
    private static AudioClip GetMusicClip(Music song)
    {
        foreach (AudioAssets.MusicAudioClip musicAudioClip in AudioAssets.instance.musicAudioClipArray)
        {
            if (musicAudioClip.song == song)
            {
                return musicAudioClip.audioClip;
            }
        }

        Debug.LogError("Song " + song + " was not found");
        return null;
    }
    #endregion
    public static void Reset()
    {
        musicObjects.Clear();
        oneShotAudioSource = null;
        oneShotGameObject = null;
    }
}
