using Scriptable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _endscreen;
    [SerializeField] private GameObject _inGameUI;

    [Header("EndScreenInfo")]
    [SerializeField] private TMP_Text _songName;
    [SerializeField] private TMP_Text _endScore;
    [SerializeField] private TMP_Text _thScore;
    [SerializeField] private TMP_Text _phScore;
    [SerializeField] private TMP_Text _ghScore;
    [SerializeField] private TMP_Text _missScore;
    [SerializeField] private TMP_Text _mcScore;
    [SerializeField] private TMP_Text _enteredUserName;

    [Header("Countdown")]
    [SerializeField] private GameObject _countdown;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private Conductor _conductor;

    [Header("IngameUI")]
    [SerializeField] private TMP_Text _scoreCount;
    [SerializeField] private TMP_Text _comboCount;
    [SerializeField] private Image _momentumBar;

    [Header("Generated Objects")]
    [SerializeField] private GameObject _scoreInformation;
    [SerializeField] private GameObject _scoreParent;
    private List<GameObject> _filledScoreChilds;

    private MusicManager _musicManager;
    private PlayerController _playerController;

    public TMP_Text EnteredUserName { get => _enteredUserName;}

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = false;
        base.Awake();

        _musicManager = FindObjectOfType<MusicManager>();
        _playerController = FindObjectOfType<PlayerController>();
        _filledScoreChilds = new List<GameObject>();

        StartCoroutine(StartCountdown());
    }

    private void OnDisable()
    {
        PointManager.Instance.ComboCounter.ChangeValue -= UpdateComboCounter;
        PointManager.Instance.MomentumCounter.ChangeValue -= UpdateMomentumBar;
        PointManager.Instance.ScoreCounter.ChangeValue -= UpdateScore;
    }
    #endregion

    #region Methods

    public void OpenEndscreen(bool wonGame, LevelInfo currentLevel)
    {
        if (_endscreen != null)
        {
            _endscreen.SetActive(true);
            _inGameUI.SetActive(false);
            GameManager.Instance.PauseGame();
            if (_musicManager is not null)
                _musicManager.LastCreatedMusicObject.StopSound();
            _playerController.ResetAllHitAreas();
            SetEndScreenInfo();
            CreateLevelScores();

            if (wonGame && currentLevel != null)
            {
                currentLevel.UnlockNextDifficulty();
            }
        }
    }

    private void CreateLevelScores()
    {
        List<ScoreInfo> scores = GameManager.Instance.ActiveLevel.ScoreCollection;

        for (int i = 0; i < scores.Count; i++)
        {
            ScoreInfo tmpScore = scores[i];
            GameObject tmp = Instantiate(_scoreInformation, _scoreParent.transform);
            _filledScoreChilds.Add(tmp);

            TMP_Text[] scoreTxtComponents = tmp.GetComponentsInChildren<TMP_Text>();

            scoreTxtComponents[0].text = $"#{tmpScore.Placement}: {tmpScore.Score}";
            scoreTxtComponents[1].text = tmpScore.PlayerName;
        }
    }

    public void GoBackToMenu()
    {
        DeleteScorePrefabs();
        PointManager.Instance.ResetLevelPoints();
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("LevelSelection");
    }

    private void DeleteScorePrefabs()
    {
         
        for (int i = 0; i < _filledScoreChilds.Count; i++)
        {
            Destroy(_filledScoreChilds[i]);
        }
        _filledScoreChilds.Clear();
    }

    private void SetEndScreenInfo()
    {
        PointManager pointManager = PointManager.Instance;
        _songName.text = GameManager.Instance.ActiveLevel.name;
        _endScore.text = _scoreCount.text;
        _thScore.text = (pointManager.PerfectNodes.Value + pointManager.GoodNodes.Value).ToString();
        _phScore.text = pointManager.PerfectNodes.Value.ToString();
        _ghScore.text = pointManager.GoodNodes.Value.ToString();
        _missScore.text = pointManager.MissedNodes.Value.ToString();
        _mcScore.text = pointManager.HighestCombo.ToString();
    }

    private void UpdateComboCounter(int value)
    {
        _comboCount.text = $"{value}x";
    }

    private void UpdateMomentumBar(float value)
    {
        _momentumBar.fillAmount = value;
    }

    private void UpdateScore(float value)
    {
        _scoreCount.text = value.ToString();
    }

    #endregion

    #region Enumerators

    private IEnumerator StartCountdown() 
    {

        for (int i = 0; i < _conductor.MusicOffset; i++)
        {
            _countdownText.text = (_conductor.MusicOffset - i).ToString();

            yield return new WaitForSeconds(1);
        }
        PointManager.Instance.ComboCounter.ChangeValue += UpdateComboCounter;
        PointManager.Instance.MomentumCounter.ChangeValue += UpdateMomentumBar;
        PointManager.Instance.ScoreCounter.ChangeValue += UpdateScore;
        _countdown.SetActive(false);
    }

    #endregion
}