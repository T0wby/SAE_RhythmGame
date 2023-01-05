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
    [SerializeField] private GameObject _levelInformation;
    [SerializeField] private GameObject _scoreInformation;
    [SerializeField] private GameObject _levelParent;
    [SerializeField] private GameObject _scoreParent;

    [Header("LevelInfo Objects")]
    [SerializeField] private Button _easyButton;
    [SerializeField] private Button _normalButton;
    [SerializeField] private Button _hardButton;
    [SerializeField] private GameObject _emptyScore;
    [SerializeField] private GameObject _filledScore;
    private List<GameObject> _filledScoreChilds;


    private void Awake()
    {
        _filledScoreChilds = new List<GameObject>();
        for (int i = 0; i < _levelCollection.Length; i++)
        {
            LevelInfo levelInfo = _levelCollection[i];
            GameObject tmp = Instantiate(_levelInformation, _levelParent.transform);
            TMP_Text[] txtComponents = tmp.GetComponentsInChildren<TMP_Text>();

            //Setting values
            txtComponents[0].text = levelInfo.SongName;
            txtComponents[1].text = levelInfo.ArtistName;
            txtComponents[2].text = levelInfo.LevelDifficulty.ToString();

            //Setting OnClick Event
            Button levelButton = tmp.GetComponent<Button>();
            if (levelButton is not null)
            {
                switch (levelInfo.LevelDifficulty)
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
                levelButton.onClick.AddListener(() =>
                {
                    LoadScores(levelInfo);
                });
            }

            //Disable Button if Level not unlocked yet
            levelButton.interactable = _levelCollection[i].IsUnlocked;
        }
    }

    #region Functions

    /// <summary>
    /// Loads Scores of a Level if it has any
    /// </summary>
    /// <param name="levelInfo">Level that the cores will be loaded from</param>
    private void LoadScores(LevelInfo levelInfo)
    {
        List<ScoreInfo> scoreInfos = levelInfo.ScoreCollection;
        int scoreCount = scoreInfos.Count;
        if (scoreCount <= 0)
        {
            EmptyScores();
        }
        else
        {
            FillScores(scoreInfos, scoreCount);
        }
    }

    /// <summary>
    /// Empties the scores and sets the UI to empty
    /// </summary>
    private void EmptyScores()
    {
        _emptyScore.SetActive(true);
        _filledScore.SetActive(false);

        for (int i = 0; i < _filledScoreChilds.Count; i++)
        {
            Destroy(_filledScoreChilds[i]);
        }
        _filledScoreChilds.Clear();
    }

    /// <summary>
    /// Adds all the available scores to the UI and activates it
    /// </summary>
    /// <param name="scoreInfos">List of scores that will be added</param>
    /// <param name="scoreCount">Number of scores in the List</param>
    private void FillScores(List<ScoreInfo> scoreInfos, int scoreCount)
    {
        for (int i = 0; i < scoreCount; i++)
        {
            GameObject tmp = Instantiate(_scoreInformation, _scoreParent.transform);
            _filledScoreChilds.Add(tmp);
            TMP_Text[] txtComponents = tmp.GetComponentsInChildren<TMP_Text>();

            //Setting values
            txtComponents[0].text = $"#{scoreInfos[i].Placement}";
            txtComponents[1].text = scoreInfos[i].PlayerName;
            txtComponents[2].text = $"{scoreInfos[i].Accuracy}%";
            txtComponents[3].text = $"{scoreInfos[i].Score}";
        }

        _emptyScore.SetActive(false);
        _filledScore.SetActive(true);
    }

    #endregion
}
