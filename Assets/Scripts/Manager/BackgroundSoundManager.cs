using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip menuBackgroundClip;
    public AudioClip fightBackgroundClip;


    public static BackgroundSoundManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuBackgroundSound()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = menuBackgroundClip;
        audioSource.Play();
    }

    public void PlayFightBackgroundSound()
    { 
        audioSource.volume = 0.3f;
        audioSource.clip = fightBackgroundClip;
        audioSource.Play();
    }
}
