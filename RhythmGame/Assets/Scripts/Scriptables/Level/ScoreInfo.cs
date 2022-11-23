using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable/Level/LevelInfo")]
    public class ScoreInfo : ScriptableObject
    {
        [SerializeField] private int _placement;
        [SerializeField] private string _playerName;
        [SerializeField] private float _accuracy;
        [SerializeField] private float _score;

    }
}
