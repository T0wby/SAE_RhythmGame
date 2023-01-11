using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private MusicManager _musicManager;

    public TMP_Text EnteredUserName { get => _enteredUserName;}

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = false;
        base.Awake();

        _musicManager = FindObjectOfType<MusicManager>();


        StartCoroutine(StartCountdown());
    }

    private void OnDisable()
    {
        PointManager.Instance.ComboCounter.ChangeValue -= UpdateComboCounter;
    }
    #endregion

    #region Methods

    public void OpenEndscreen()
    {
        _endscreen.SetActive(true);
        GameManager.Instance.PauseGame();
        if (_musicManager is not null)
            _musicManager.LastCreatedMusicObject.StopSound();
        SetEndScreenInfo();
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
        _songName.text = GameManager.Instance.ActiveLevel;
        //Score _endScore
        _thScore.text = (pointManager.PerfectNodes.Value + pointManager.GoodNodes.Value).ToString();
        _phScore.text = pointManager.PerfectNodes.Value.ToString();
        _ghScore.text = pointManager.GoodNodes.Value.ToString();
        _missScore.text = pointManager.MissedNodes.Value.ToString();
        //_mcScore.text =
    }

    private void UpdateComboCounter(int value)
    {
        _comboCount.text = $"{value}x";
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
        _countdown.SetActive(false);
    }

    #endregion
}
