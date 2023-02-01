using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OptionSetting : MonoBehaviour
{
    public AudioMixer audioMaster;
    public AudioMixer audioMusic;
    public AudioMixer audioSFX;
    public void SetMasterVolume(float Volume) 
    {
        audioMaster.SetFloat("volumeMaster", Volume);
    }
    public void SetMusicVolume(float Volume)
    {
        audioMaster.SetFloat("volumeMusic", Volume);
    }
    public void SetSFXVolume(float Volume)
    {
        audioMaster.SetFloat("volumeSFX", Volume);
    }
}
