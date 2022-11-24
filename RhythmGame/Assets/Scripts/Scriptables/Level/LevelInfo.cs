using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable/Level/LevelInfo")]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] private string _songName;
        [SerializeField] private string _artistName;
        [SerializeField] private bool _isUnlocked;
        [SerializeField] private float _price;
        [SerializeField] private ELevelDifficulty _levelDifficulty;
        [SerializeField] private List<ScoreInfo> _scoreCollection;

        public string SongName => _songName;
        public string ArtistName => _artistName;
        public bool IsUnlocked => _isUnlocked;
        public float Price => _price;
        public ELevelDifficulty LevelDifficulty => _levelDifficulty;
        public List<ScoreInfo> ScoreCollection => _scoreCollection;
    } 
}
