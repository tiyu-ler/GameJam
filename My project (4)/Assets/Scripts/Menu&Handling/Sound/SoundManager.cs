using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }

    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)] public static float MusicVolume = 0.5f;
    [Range(0f, 1f)] public static float AmbientVolume = 0.5f;
    [Range(0f, 1f)] public static float SoundVolume = 0.5f;

    public SoundClass[] sounds;
    private Dictionary<string, SoundClass> soundDictionary;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSounds();
    }

    private void InitializeSounds()
    {
        soundDictionary = new Dictionary<string, SoundClass>();

        foreach (SoundClass s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
            soundDictionary[s.name] = s;
        }
    }

    public void Play(string sound)
    {
        if (soundDictionary.TryGetValue(sound, out SoundClass s))
        {
            s.source.pitch = s.pitch * UnityEngine.Random.Range(1f - s.pitchVariance / 2f, 1f + s.pitchVariance / 2f);

            UpdateSourceVolume(s);
            s.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound: {sound} not found!");
        }
    }

    public void Stop(string sound)
    {
        if (soundDictionary.TryGetValue(sound, out SoundClass s) && s.source != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning($"Sound: {sound} not found!");
        }
    }

    public void UpdateMusicVolume(float musVolume)
    {
        MusicVolume = musVolume;
        UpdateVolumes();
    }

    public void UpdateAmbientVolume(float ambVolume)
    {
        AmbientVolume = ambVolume;
        UpdateVolumes();
    }

    public void UpdateSoundVolume(float sndVolume)
    {
        SoundVolume = sndVolume;
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        foreach (var s in soundDictionary.Values)
        {
            if (s.source != null && s.source.isPlaying)
            {
                UpdateSourceVolume(s);
            }
        }
    }

    private void UpdateSourceVolume(SoundClass s)
    {
        // Adjust volume based on type
        float volume = s.volume * UnityEngine.Random.Range(1f - s.volumeVariance / 2f, 1f + s.volumeVariance / 2f);
        if (s.Music)
        {
            s.source.volume = volume * MusicVolume;
        }
        else if (s.Sound)
        {
            s.source.volume = volume * SoundVolume;
        }
        else if (s.Ambient)
        {
            s.source.volume = volume * AmbientVolume;
        }
    }
}