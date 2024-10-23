using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Source----------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;


    [Header("-------Audio Clip----------")]
    public AudioClip background;
    public AudioClip ghostFlameSoundTrack;
    public AudioClip growBody;
    public AudioClip enterPortal;
    public AudioClip playerGettingHit;
    public AudioClip firingProjectile;
    //public AudioClip firingRangedSlash;
    //public AudioClip firingHellFire;
    //public AudioClip firingIreberg;
    public AudioClip enemyHit;
    public AudioClip summoningGlyph;
    public AudioClip bossKilled;


    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
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
        musicSource.clip= background;
        musicSource.Play();
    }

    public void PlayBossMusic()
    {
        musicSource.clip = ghostFlameSoundTrack;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
