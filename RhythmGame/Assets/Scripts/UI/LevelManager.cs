using Scriptable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Collection of available Levels")]
    [SerializeField] private LevelInfo[] _levelCollection = null;

    [Header("Generated Objects")]
    [SerializeField] GameObject _levelInformation;
    [SerializeField] GameObject _scoreInformation;
    [SerializeField] GameObject _levelParent;
    [SerializeField] GameObject _scoreParent;

    [Header("LevelInfo Objects")]
    [SerializeField] Button _easyButton;
    [SerializeField] Button _normalButton;
    [SerializeField] Button _hardButton;


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

            //Setting OnClick Event

            Button levelButton = tmp.GetComponent<Button>();
            if (levelButton is not null)
            {
                switch (_levelCollection[i].LevelDifficulty)
                {
                    case ELevelDifficulty.EASY:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = false;
                            _hardButton.interactable = false;
                            GameManager.Instance.ActiveLevel = txtComponents[0].text;
                        });
                        break;
                    case ELevelDifficulty.NORMAL:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = true;
                            _hardButton.interactable = false;
                            GameManager.Instance.ActiveLevel = txtComponents[0].text;
                        });
                        break;
                    case ELevelDifficulty.HARD:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = true;
                            _hardButton.interactable = true;
                            GameManager.Instance.ActiveLevel = txtComponents[0].text;
                        });
                        break;
                    default:
                        break;
                }

            }

            //Disable Button if Level not unlocked yet
            levelButton.interactable = _levelCollection[i].IsUnlocked;
        }
    }
}
