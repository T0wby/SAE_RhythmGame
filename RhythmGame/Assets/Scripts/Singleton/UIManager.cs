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
            GameManager.Instance.PauseGame();
            if (_musicManager is not null)
                _musicManager.LastCreatedMusicObject.StopSound();
            // Might want to switch the Reset of all Hit
            _playerController.ResetAllHitAreas();
            SetEndScreenInfo();

            if (wonGame && currentLevel != null)
            {
                currentLevel.UnlockNextDifficulty();
            }
        }
    }

    public void GoBackToMenu()
    {
        PointManager.Instance.ResetLevelPoints();
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("LevelSelection");
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
