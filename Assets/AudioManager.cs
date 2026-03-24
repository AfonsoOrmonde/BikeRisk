using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] List<Sound> SfxSounds;
    [SerializeField] List<Sound> MusicSounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameObject persistentRoot = transform.root.gameObject;
        DontDestroyOnLoad(persistentRoot);
    }


    public void playSFX(String name)
    {
        Sound sound = SfxSounds.Find(x => x.name == name);

        if (sound == null)
        {
            Debug.LogWarning($"No sound with name: {name}");
            return;
        }

        sfxSource.clip = sound.clip;
        sfxSource.Play();
    }
    public void playMusic(String name)
    {
        Sound sound = MusicSounds.Find(x => x.name == name);

        if (sound == null)
        {
            Debug.LogWarning($"No music with name: {name}");
            return;
        }

        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void changeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }

    public void changeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }


    public float getMusicVolume()
    {
        return musicSource.volume;
    }

    public float getSFXVolume()
    {
        return sfxSource.volume;
    }

}
