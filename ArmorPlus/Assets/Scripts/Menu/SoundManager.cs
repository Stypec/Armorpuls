using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;    

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        Sound s = Array.Find(musicSounds, x => x.meno == meno);

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
}
