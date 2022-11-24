using Scriptable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Collection of available Levels")]
    [SerializeField] private LevelInfo[] _levelCollection = null;

    [Header("Generated Objects")]
    [SerializeField] GameObject _levelInformation;
    [SerializeField] GameObject _scoreInformation;
    [SerializeField] GameObject _levelParent;
    [SerializeField] GameObject _scoreParent;

    private void Awake()
    {
        for (int i = 0; i < _levelCollection.Length; i++)
        {
            GameObject tmp = Instantiate(_levelInformation, _levelParent.transform);
            TMP_Text[] txtComponents = tmp.GetComponentsInChildren<TMP_Text>();

            //Setting values
            txtComponents[0].text = _levelCollection[i].SongName;
            txtComponents[1].text = _levelCollection[i].ArtistName;
            txtComponents[2].text = _levelCollection[i].LevelDifficulty.ToString();
        }
    }
}
