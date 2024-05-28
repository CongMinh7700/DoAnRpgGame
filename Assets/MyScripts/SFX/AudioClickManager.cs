using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClickManager : RPGMonoBehaviour
{
    protected static AudioClickManager instance;
    public static AudioClickManager Instance => instance;
    [SerializeField] protected AudioSource audioSource;
    protected override void LoadComponents()
    {
        LoadSingleton();
        LoadAudioSource();
    }
    protected void LoadSingleton()
    {
        if (AudioClickManager.instance != null) Debug.LogWarning("Only 1 AudioClickManager allow to exist");
        AudioClickManager.instance = this;
    }
    protected void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
        
    }
    public void PlaySFXClick()
    {
        audioSource.Play();
    }
}
