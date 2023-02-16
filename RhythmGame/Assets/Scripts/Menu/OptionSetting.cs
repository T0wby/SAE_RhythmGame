using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class OptionSetting : MonoBehaviour
{
    public AudioMixer audioMaster;
    public AudioMixer audioMusic;
    public AudioMixer audioSFX;
    public TMP_Dropdown resoulutionDropdown;
    public TMP_Text travelTimeValue;
    public Slider travelTimeSlider;
    Resolution[] resolutions;

    public void Start()
    {
        List<string> options = new List<string>();

        resolutions = Screen.resolutions;
        resoulutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);
        }
        resoulutionDropdown.AddOptions(options);

        travelTimeSlider.value = GameManager.Instance.TravelTime;
        travelTimeValue.text = $"{GameManager.Instance.TravelTime} sec";
    }
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
    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetTravelTimeValue(float value)
    {
        travelTimeValue.text = $"{value} sec";
        GameManager.Instance.TravelTime = value;
    }
}
