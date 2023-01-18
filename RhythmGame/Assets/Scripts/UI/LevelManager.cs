using Scriptable;
using System;
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
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _emptyScore;
    [SerializeField] private GameObject _filledScore;
    private List<GameObject> _filledScoreChilds;

    [Header("Text")]
    [SerializeField] private TMP_Text _experienceCounter;


    private void Awake()
    {
        SetDifficultyEvents();
        SetExperienceCounter();
        CreateLevel();
    }

    private void OnEnable()
    {
        GameManager.Instance.ExperiencePoints.ChangeValue += SetExperienceEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.ExperiencePoints.ChangeValue -= SetExperienceEvent;
    }

    #region Methods


    /// <summary>
    /// Creates our level from the _levelCollection array
    /// </summary>
    private void CreateLevel()
    {
        _filledScoreChilds = new List<GameObject>();
        for (int i = 0; i < _levelCollection.Length; i++)
        {
            LevelInfo levelInfo = _levelCollection[i];
            GameObject tmp = Instantiate(_levelInformation, _levelParent.transform);
            GameObject levelprefab = tmp.transform.GetChild(0).gameObject;
            GameObject saleprefab = tmp.transform.GetChild(1).gameObject;


            TMP_Text[] levelTxtComponents = levelprefab.GetComponentsInChildren<TMP_Text>();
            TMP_Text[] saleTxtComponents = saleprefab.GetComponentsInChildren<TMP_Text>();

            //Setting values
            levelTxtComponents[0].text = levelInfo.SongName;
            levelTxtComponents[1].text = levelInfo.ArtistName;
            levelTxtComponents[2].text = levelInfo.LevelDifficulty.ToString();
            saleTxtComponents[1].text = $"{levelInfo.Price} Exp";

            //Setting OnClick Event depending of the unlocked Difficulty
            Button levelButton = levelprefab.GetComponent<Button>();

            if (levelButton is not null)
            {
                switch (levelInfo.LevelDifficulty)
                {
                    case ELevelDifficulty.EASY:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = false;
                            _hardButton.interactable = false;
                        });
                        break;
                    case ELevelDifficulty.NORMAL:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = true;
                            _hardButton.interactable = false;
                        });
                        break;
                    case ELevelDifficulty.HARD:
                        levelButton.onClick.AddListener(() =>
                        {
                            _normalButton.interactable = true;
                            _hardButton.interactable = true;
                        });
                        break;
                    default:
                        break;
                }
                levelButton.onClick.AddListener(() =>
                {
                    GetActiveLevel(levelTxtComponents[0].text);
                    _startButton.interactable = true;
                    LoadScores(levelInfo);
                });
            }

            //Disable Button if Level not unlocked yet
            if (levelInfo.IsUnlocked)
            {
                levelprefab.SetActive(true);
                saleprefab.SetActive(false);
            }
            else
            {
                levelButton.interactable = false;
                levelprefab.SetActive(false);
                saleprefab.SetActive(true);

                Button saleButton = saleprefab.GetComponent<Button>();
                saleButton.onClick.AddListener(() =>
                {
                    if (TryToBuyLevel(levelInfo.Price))
                    {
                        levelInfo.IsUnlocked = true;
                        levelprefab.SetActive(true);
                        levelButton.interactable = true;
                        saleprefab.SetActive(false);
                    }
                });
            }
        }
    }

    /// <summary>
    /// Sets the current active level
    /// </summary>
    /// <param name="lvlName">Name of the level</param>
    private void GetActiveLevel(string lvlName)
    {
        for (int i = 0; i < _levelCollection.Length; i++)
        {
            if (_levelCollection[i].name.ToLower() == lvlName.ToLower())
            {
                GameManager.Instance.ActiveLevel = _levelCollection[i];
                break;
            }
        }
    }

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
        EmptyScores();
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

    /// <summary>
    /// Sets onclick events to our Buttons for the difficulty
    /// </summary>
    private void SetDifficultyEvents()
    {
        GameManager gameManager = GameManager.Instance;

        _easyButton.onClick.AddListener(() => gameManager.CurrentLevelDifficulty = ELevelDifficulty.EASY);
        _normalButton.onClick.AddListener(() => gameManager.CurrentLevelDifficulty = ELevelDifficulty.NORMAL);
        _hardButton.onClick.AddListener(() => gameManager.CurrentLevelDifficulty = ELevelDifficulty.HARD);
    }

    /// <summary>
    /// Updates UI on value change from our experience
    /// </summary>
    /// <param name="newValue"></param>
    private void SetExperienceEvent(float newValue)
    {
        if (_experienceCounter != null)
        {
            _experienceCounter.text = newValue.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetExperienceCounter()
    {
        _experienceCounter.text = GameManager.Instance.ExperiencePoints.Value.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    private bool TryToBuyLevel(float price)
    {
        if (GameManager.Instance.ExperiencePoints.Value > price)
        {
            GameManager.Instance.ExperiencePoints.Value -= price;
            return true;
        }
        return false;
    }
    #endregion
}
