using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : RPGMonoBehaviour
{
    protected static SFXManager instance;
    public static SFXManager Instance => instance;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip pickUp;
    [SerializeField] protected AudioClip click;
    [SerializeField] protected AudioClip impact;
    [SerializeField] protected AudioClip fireImpact;
    [SerializeField] protected AudioClip iceImpact;
    protected override void LoadComponents()
    {
        LoadSingleton();
        LoadAudioSource();
    }
    protected void LoadSingleton()
    {
        if (SFXManager.instance != null) Debug.LogWarning("Only 1 AudioClickManager allow to exist");
        SFXManager.instance = this;
    }
    protected void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
        
    }
    public void PlaySFXClick()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    public void PlaySFXPickUp()
    {
        audioSource.clip = pickUp;
        audioSource.Play();
    }
    public void PlaySFXImpact()
    {
        audioSource.clip = impact;
        audioSource.Play();
    }
    public void PlayFireImpact()
    {
        audioSource.clip = fireImpact;
        audioSource.Play();
    }
    public void PlayIceImpact()
    {
        audioSource.clip = iceImpact;
        audioSource.Play();
    }
}
