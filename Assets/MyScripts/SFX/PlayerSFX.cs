using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : RPGMonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] protected AudioClip[] weaponSounds;
    [SerializeField] protected AudioClip skillSound;
    protected override void LoadComponents()
    {
        LoadAudioSource();
        
    }
    protected virtual void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();

    }
    public virtual void SetWeaponSFX(int index)
    {
        audioSource.clip = weaponSounds[index];
    }
    public virtual void SetSkillSFX()
    {
        audioSource.clip = skillSound;
    }

}
