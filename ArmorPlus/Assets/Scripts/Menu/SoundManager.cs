using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;    

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MenuTheme");
    }

    public void PlayMusic(string meno)
    {
        Sound s = Array.Find(musicSounds, x=> x.meno == meno);

        if(s == null)
        {
            Debug.Log("Sound not found.");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string meno)
    {
        Sound s = Array.Find(sfxSounds, x => x.meno == meno);

        if (s == null)
        {
            Debug.Log("Sound not found.");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            sfxSource.Play();
        }

    }

    public void Musicstop()
    {
        SoundManager.Instance.musicSource.Stop();
    }

    public void ToogleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToogleSound()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SoundVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
