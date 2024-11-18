using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;
    public string backgroundMusicName;
    public string audioFolderPath = "Audio";

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameObject audioSourcesObject = GameObject.FindWithTag("AudioSources");
        if (audioSourcesObject == null)
        {
            audioSourcesObject = new GameObject("AudioSources");
            audioSourcesObject.tag = "AudioSources";
            audioSourcesObject.transform.SetParent(transform);
        }

        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = audioSourcesObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true;
        }

        if (sfxSource == null)
        {
            sfxSource = audioSourcesObject.AddComponent<AudioSource>();
        }

        LoadAudioClips();

        PlayBackgroundMusic(backgroundMusicName);
    }

    void LoadAudioClips()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(audioFolderPath);
        foreach (AudioClip clip in clips)
        {
            audioClips[clip.name] = clip;
        }

        foreach (AudioClip audioClip in audioClips.Values)
        {
            Debug.Log(audioClip.name);
        }
    }

    public void PlayBackgroundMusic(string name)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip not found: " + name);
        }
    }

    public void PlaySFX(string name)
    {
        PlaySFX(name, 1f);
    }

    public void PlaySFX(string name, float volume)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }
}