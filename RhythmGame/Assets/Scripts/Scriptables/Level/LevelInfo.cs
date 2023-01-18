using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

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
        public float Price => _price;
        public ELevelDifficulty LevelDifficulty => _levelDifficulty;
        public List<ScoreInfo> ScoreCollection => _scoreCollection;
        public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }

        public void Init(string songName, string artistName, bool isUnlocked, float price, ELevelDifficulty levelDifficulty, List<ScoreInfo> scoreCollection)
        {
            _songName = songName;
            _artistName = artistName;
            _isUnlocked = isUnlocked;
            _price = price;
            _levelDifficulty = levelDifficulty;
            _scoreCollection = scoreCollection;
        }

        #region Methods

        #region Sorting
        // Currently BubbleSort
        public void SortScoreCollection()
        {
            ScoreInfo tmp;
            int length = _scoreCollection.Count;
            for (int i = 0; i < length; i++)
            {
                for (int n = 0; n < length - 1 - i; n++)
                {
                    if (_scoreCollection[n].Score < _scoreCollection[n + 1].Score)
                    {
                        tmp = _scoreCollection[n + 1];
                        _scoreCollection[n + 1] = _scoreCollection[n];
                        _scoreCollection[n] = tmp;
                    }
                }
            }
            SetPlacement(length, _scoreCollection);
        }

        private void SetPlacement(int length, List<ScoreInfo> scoreCollection)
        {
            for (int i = 0; i < length; i++)
            {
                scoreCollection[i].Placement = i + 1;
            }
        }
        #endregion

        public void UnlockNextDifficulty()
        {
            switch (_levelDifficulty)
            {
                case ELevelDifficulty.EASY:
                    _levelDifficulty = ELevelDifficulty.NORMAL;
                    break;
                case ELevelDifficulty.NORMAL:
                    _levelDifficulty = ELevelDifficulty.HARD;
                    break;
                default:
                    break;
            }
        }

        #endregion
    } 
}
