using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSerializable
{
    private string _songName;
    private string _artistName;
    private bool _isUnlocked;
    private float _price;
    private ELevelDifficulty _levelDifficulty;
    private List<ScoreSerializable> _scoreCollection;

    public string SongName => _songName;
    public string ArtistName => _artistName;
    public bool IsUnlocked => _isUnlocked;
    public float Price => _price;
    public ELevelDifficulty LevelDifficulty => _levelDifficulty;
    public List<ScoreSerializable> ScoreCollection => _scoreCollection;

    public LevelSerializable(string songName, string artistName, bool isUnlocked, float price, ELevelDifficulty levelDifficulty, List<ScoreSerializable> scoreCollection)
    {
        _songName = songName;
        _artistName = artistName;
        _isUnlocked = isUnlocked;
        _price = price;
        _levelDifficulty = levelDifficulty;
        _scoreCollection = scoreCollection;
    }
}
