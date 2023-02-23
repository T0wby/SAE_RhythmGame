using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class OptionSetting : MonoBehaviour
{
    public AudioMixer audioMaster;
    public AudioMixer audioMusic;
    public AudioMixer audioSFX;
    public TMP_Dropdown resoulutionDropdown;
    public TMP_Text travelTimeValue;
    public Slider audioMasterSlider;
    public Slider audioMusicSlider;
    public Slider audioSFXSlider;
    public Slider travelTimeSlider;
    public Toggle fullscreenToggle;
    Resolution[] resolutions;
    public GameSettings gameSettings;

    public void Start()
    {
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        resoulutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resoulutionDropdown.AddOptions(options);
        resoulutionDropdown.value = currentResolutionIndex;
        resoulutionDropdown.RefreshShownValue();
        SetSettings();
    }

    public void SetSettings()
    {
        //Audio
        audioMaster.SetFloat("volumeMaster", Mathf.Log10(gameSettings.MasterVolume) * 20);
        audioMasterSlider.value = gameSettings.MasterVolume;
        audioMaster.SetFloat("volumeMusic", Mathf.Log10(gameSettings.MusicVolume) * 20);
        audioMusicSlider.value = gameSettings.MusicVolume;
        audioMaster.SetFloat("volumeSFX", Mathf.Log10(gameSettings.SFXVolume) * 20);
        audioSFXSlider.value = gameSettings.SFXVolume;

        //Quality
        QualitySettings.SetQualityLevel(gameSettings.QualityIndex);

        //Fullscreen
        fullscreenToggle.isOn = gameSettings.IsFullscreen;
        Screen.fullScreen = gameSettings.IsFullscreen;

        //Gameplay
        travelTimeSlider.value = gameSettings.TravelTimeValue;
        travelTimeValue.text = $"{gameSettings.TravelTimeValue} sec";
        GameManager.Instance.TravelTime = gameSettings.TravelTimeValue;
    }

    public void SaveSettings()
    {
        SaveGameManager.Instance.SaveGameSettings();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float Volume) 
    {
        audioMaster.SetFloat("volumeMaster", Mathf.Log10(Volume) * 20);
        gameSettings.MasterVolume = Volume;
    }
    public void SetMusicVolume(float Volume)
    {
        audioMaster.SetFloat("volumeMusic", Mathf.Log10(Volume) * 20);
        gameSettings.MusicVolume = Volume;
    }
    public void SetSFXVolume(float Volume)
    {
        audioMaster.SetFloat("volumeSFX", Mathf.Log10(Volume) * 20);
        gameSettings.SFXVolume = Volume;
    }
    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        gameSettings.QualityIndex = qualityIndex;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        gameSettings.IsFullscreen = isFullscreen;
    }
    public void SetTravelTimeValue(float value)
    {
        travelTimeValue.text = $"{value} sec";
        GameManager.Instance.TravelTime = value;
        gameSettings.TravelTimeValue = value;
    }
}
