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
    private MusicManager _musicManager;

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = false;
        base.Awake();

        _musicManager = FindObjectOfType<MusicManager>();
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
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("LevelSelection");
    }

    private void SetEndScreenInfo()
    {
        _songName.text = GameManager.Instance.ActiveLevel;
        //Score _endScore
        //_thScore.text =
        _phScore.text = PointManager.Instance.PerfectNodes.Value.ToString();
        _ghScore.text = PointManager.Instance.GoodNodes.Value.ToString();
        _missScore.text = PointManager.Instance.MissedNodes.Value.ToString();
        //_mcScore.text =
    }

    #endregion
}
