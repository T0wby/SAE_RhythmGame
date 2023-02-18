using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [SerializeField] private Float _experiencePoints;
    private LevelInfo _activeLevel;
    private ELevelDifficulty _currentLevelDifficulty = ELevelDifficulty.EASY;
    private List<float> _spawnerOne = new List<float>();
    private List<float> _spawnerTwo = new List<float>();
    private List<float> _spawnerThree = new List<float>();
    private List<float> _spawnerFour = new List<float>();
    private bool _isPaused = false;
    private Conductor _conductor;
    private float _travelTime = 3f;
    #endregion

    #region Properties


    public List<float> SpawnerOne { get { return _spawnerOne; } set { _spawnerOne = value; } }
    public List<float> SpawnerTwo { get { return _spawnerTwo; } set { _spawnerTwo = value; } }
    public List<float> SpawnerThree { get { return _spawnerThree; } set { _spawnerThree = value; } }
    public List<float> SpawnerFour { get { return _spawnerFour; } set { _spawnerFour = value; } }
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public Float ExperiencePoints { get => _experiencePoints; }
    public Conductor Conductor { get => _conductor; set => _conductor = value; }
    public LevelInfo ActiveLevel { get => _activeLevel; set => _activeLevel = value; }
    public ELevelDifficulty CurrentLevelDifficulty { get => _currentLevelDifficulty; set => _currentLevelDifficulty = value; }
    public float TravelTime { get => _travelTime; set => _travelTime = value; }

    #endregion


    #region Methods

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = true;
        base.Awake();
        _experiencePoints.ChangeValue += SaveExp;
    }
    #endregion

    #region Functions

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void UnPauseGame() 
    {
        _isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void EndGame(bool wonGame)
    {
        _experiencePoints.Value += PointManager.Instance.ScoreCounter.Value;
        _conductor.StopConductor();
        UIManager.Instance.OpenEndscreen(wonGame, _activeLevel);
    }

    private void SaveExp(float value)
    {
        SaveGameManager.Instance.SaveExpInformation(_experiencePoints);
    }

    #endregion

    #endregion
}
