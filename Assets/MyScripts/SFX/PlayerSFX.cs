using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : RPGMonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip[] weaponSounds;
    [SerializeField] protected AudioClip[] skillSounds;
    protected override void LoadComponents()
    {
        LoadAudioSource();
        
    }
    protected virtual void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();

    }
    protected virtual void SetWeaponSFX(int index)
    {
        audioSource.clip = weaponSounds[index];
    }
    protected virtual void SetSkillSFX(int index)
    {
        audioSource.clip = skillSounds[index];
    }
    public virtual void PlaySound()
    {
        audioSource.Play();
    }
}
