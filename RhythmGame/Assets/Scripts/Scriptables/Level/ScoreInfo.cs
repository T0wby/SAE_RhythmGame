using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "NewScoreInfo", menuName = "Scriptable/Level/ScoreInfo")]
    public class ScoreInfo : ScriptableObject
    {
        [SerializeField] private int _placement;
        [SerializeField] private string _playerName;
        [SerializeField] private float _accuracy;
        [SerializeField] private float _score;

        public int Placement { get => _placement; set => _placement = value; }
        public string PlayerName { get => _playerName; set => _playerName = value; }
        public float Accuracy { get => _accuracy; set => _accuracy = value; }
        public float Score { get => _score; set => _score = value; }
    }
}
