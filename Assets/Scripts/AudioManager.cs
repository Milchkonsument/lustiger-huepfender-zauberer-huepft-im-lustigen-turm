using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public bool loop;
}

public class AudioManager : Singleton<AudioManager>
{

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [Range(0f, 1f)] public float MusicVolume;
    [Range(0f, 1f)] public float SFXVolume;

    public override bool DestroyOnLoad => false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PlayMusic("Menu");
        }
        else
        {
            PlayMusic("Game");
        }

    }
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "MainMenu")
        {
            PlayMusic("Menu");
        }
        else
        {
            PlayMusic("Game");
        }
    }

    public void PlayMusic(String name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
            return;

        musicSource.Pause();
        musicSource.loop = s.loop;
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(String name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
            return;

        sfxSource.Pause();
        sfxSource.loop = s.loop;
        sfxSource.clip = s.clip;
        sfxSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
