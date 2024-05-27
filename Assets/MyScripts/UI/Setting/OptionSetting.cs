using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class OptionSetting : RPGMonoBehaviour
{
    public Slider musicSlider;
    public Slider soundFxSlider;
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;
    public GameObject optionUI;
    protected override void LoadComponents()
    {
        LoadOptionUI();
    } 
    protected virtual void LoadOptionUI()
    {
        if (this.optionUI != null) return;
        this.optionUI = this.gameObject;
    }
    public void ChangeMusicValue()
    {
        musicMixer.SetFloat("MusicVolume", musicSlider.value);
    }
    public void ChangeSoundFXValue()
    {
        soundMixer.SetFloat("SFXVolume", soundFxSlider.value);
    }
    public void Close()
    {
        optionUI.SetActive(false);
    }
}
