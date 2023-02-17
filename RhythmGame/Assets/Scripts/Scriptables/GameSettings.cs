using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable/GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    public float MasterVolume = 1f;
    public float MusicVolume = 1f;
    public float SFXVolume = 1f;
    public float TravelTimeValue = 3f;
    public int QualityIndex = 1;
    public bool IsFullscreen = true;
}
