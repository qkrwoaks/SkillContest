using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip monsterDeadClip;
    public AudioClip onButtonClip;
    public AudioClip destroyMonsterClip;
    public AudioClip onDeadClip;
    public AudioClip onStageClearClip;
    public AudioClip onGameClearClip;
    public AudioClip onCellDeadClip;
    public AudioClip fireBulletClip;
    public AudioClip onDamageClip;

    public static EffectSoundManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(onButtonClip);
    }

    public void DestroyMonster()
    {
        audioSource.PlayOneShot(destroyMonsterClip);
    }

    public void PlayOnDeadSound()
    {
        BackgroundSoundManager.instance.audioSource.Stop();
        audioSource.PlayOneShot(onDeadClip);
    }

    public void PlayOnStageClearClip()
    {
        BackgroundSoundManager.instance.audioSource.Stop();
        audioSource.PlayOneShot(onStageClearClip);
    }

    public void PlayOnGameClearClip()
    {
        BackgroundSoundManager.instance.audioSource.Stop();
        audioSource.PlayOneShot(onGameClearClip);
    }

    public void PlayOnDamageToCellClip()
    {
        audioSource.PlayOneShot(onCellDeadClip);
    }

    public void FireBulletClip()
    {
        audioSource.PlayOneShot(fireBulletClip);
    }

    public void PlayOnDamageToPlayerClip()
    {
        audioSource.PlayOneShot(onDamageClip);
    }
}
