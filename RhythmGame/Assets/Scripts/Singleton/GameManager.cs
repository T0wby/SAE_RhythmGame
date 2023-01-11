using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [SerializeField] private List<GameState> _gameStates = null;
    [SerializeField] private GameState _activeState = null;
    private string _activeLevel;
    private List<float> _spawnerOne = new List<float>();
    private List<float> _spawnerTwo = new List<float>();
    private List<float> _spawnerThree = new List<float>();
    private List<float> _spawnerFour = new List<float>();
    private bool _isPaused = false;
    #endregion

    #region Properties
    public string ActiveLevel
    {
        get
        {
            return _activeLevel;
        }

        set
        {
            _activeLevel = value;
        }
    }

    public GameState ActiveState
    {
        get { return _activeState; }
    }

    public List<float> SpawnerOne { get { return _spawnerOne; } set { _spawnerOne = value; } }
    public List<float> SpawnerTwo { get { return _spawnerTwo; } set { _spawnerTwo = value; } }
    public List<float> SpawnerThree { get { return _spawnerThree; } set { _spawnerThree = value; } }
    public List<float> SpawnerFour { get { return _spawnerFour; } set { _spawnerFour = value; } }

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }

    #endregion


    #region Methods

    #region Gamestates

    public void ChangeGamestate<T>() where T : GameState
    {
        foreach (GameState state in _gameStates)
        {
            if (state.GetType() == typeof(T))
            {
                //_activeState.Finalize();
                _activeState = state;
                //_activeState.Initialize();
            }
        }
    }

    #endregion

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = true;
        base.Awake();

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

    #endregion

    #endregion
}
