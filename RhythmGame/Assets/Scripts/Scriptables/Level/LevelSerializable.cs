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
    private uint _bpm;
    private ELevelDifficulty _levelDifficulty;
    private List<ScoreSerializable> _scoreCollection;

    public string SongName => _songName;
    public string ArtistName => _artistName;
    public bool IsUnlocked => _isUnlocked;
    public float Price => _price;
    public uint Bpm => _bpm;
    public ELevelDifficulty LevelDifficulty => _levelDifficulty;
    public List<ScoreSerializable> ScoreCollection => _scoreCollection;

    public LevelSerializable(string songName, string artistName, bool isUnlocked, float price, uint bpm, ELevelDifficulty levelDifficulty, List<ScoreSerializable> scoreCollection)
    {
        _songName = songName;
        _artistName = artistName;
        _isUnlocked = isUnlocked;
        _price = price;
        _bpm = bpm;
        _levelDifficulty = levelDifficulty;
        _scoreCollection = scoreCollection;
    }
}
