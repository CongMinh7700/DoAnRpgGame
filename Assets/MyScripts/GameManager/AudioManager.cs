using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : RPGMonoBehaviour
{
    [SerializeField] public AudioClip mainSound;
    [SerializeField] public AudioClip shopSound;
    [SerializeField] protected AudioSource audioSource;
    public static bool isNpcShop;
    public static bool canPlay;
    protected override void LoadComponents()
    {
        LoadAudioSource();
    }
    protected virtual void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        canPlay = true;
    }
    void Update()
    {
        if(canPlay)
        {
            canPlay = false;
            if (isNpcShop)
            {
                audioSource.clip = shopSound;
                audioSource.volume = 0.6f;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = mainSound;
                audioSource.volume = 0.6f;
                audioSource.Play();
            }
        }     
   
    }
}
