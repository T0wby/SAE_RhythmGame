using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreSerializable
{
    private int _placement;
    private string _playerName;
    private float _accuracy;
    private float _score;

    public int Placement { get => _placement; set => _placement = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public float Accuracy { get => _accuracy; set => _accuracy = value; }
    public float Score { get => _score; set => _score = value; }

    public ScoreSerializable(int placement, string playerName, float accuracy, float score)
    {
        _placement = placement;
        _playerName = playerName;
        _accuracy = accuracy;
        _score = score;
    }
}
