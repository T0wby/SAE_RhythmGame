using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PointManager : Singleton<PointManager>
{
    [Header("Level")]
    [SerializeField] private LevelInfo[] _levelCollection = null;
    private LevelInfo _currentLevel = null;

    [Header("Points")]
    [SerializeField] private Integer _goodNodes;
    [SerializeField] private Integer _perfectNodes;
    [SerializeField] private Integer _missedNodes;
    [SerializeField] private Integer _comboCounter;
    [SerializeField] private Float _momentumCounter;
    [SerializeField] private Float _scoreCounter;
    [SerializeField, Range(0 , 1)] private float _lossPercent;
    private int _totalLevelNodes = 0;
    private float _goodNodePoints = 2.5f;
    private float _perfectNodePoints = 3.5f;
    private int _highestCombo = 0;

    [Header("Spawner")]
    [SerializeField] private Spawner _spawnerone;
    [SerializeField] private Spawner _spawnertwo;
    [SerializeField] private Spawner _spawnerthree;
    [SerializeField] private Spawner _spawnerfour;


    #region Properties
    public Integer GoodNodes { get => _goodNodes; }
    public Integer PerfectNodes { get => _perfectNodes; }
    public Integer MissedNodes { get => _missedNodes; }
    public Integer ComboCounter { get => _comboCounter; set => _comboCounter = value; }
    public Float MomentumCounter
    {
        get
        {
            return _momentumCounter;
        }
        set
        {
            if (value < 0)
                _momentumCounter.Value = 0;
            else if (value > 1)
                _momentumCounter.Value = 1;
            else
                _momentumCounter = value;
        }
    }

    public Float ScoreCounter { get => _scoreCounter; set => _scoreCounter = value; }
    public float GoodNodePoints { get => _goodNodePoints; }
    public float PerfectNodePoints { get => _perfectNodePoints; }
    public int HighestCombo { get => _highestCombo; }
    #endregion


    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = false;
        base.Awake();
    }

    private void Start()
    {
        _totalLevelNodes = _spawnerone.SpawnTimings.Count + _spawnertwo.SpawnTimings.Count + _spawnerthree.SpawnTimings.Count + _spawnerfour.SpawnTimings.Count;
        _missedNodes.ChangeValue += CheckLossCondition;
        _comboCounter.ChangeValue += CheckMaxCombo;
        _momentumCounter.ChangeValue += CheckMomentumValue;
        ResetLevelPoints();
    }
    #endregion



    #region Methods

    private void CheckMomentumValue(float newValue)
    {
        if (newValue < 0)
            _momentumCounter.Value = 0;
        else if (newValue > 1)
            _momentumCounter.Value = 1;
    }

    private void CheckLossCondition(int newValue)
    {
        if (newValue > (_totalLevelNodes * _lossPercent) && !GameManager.Instance.IsPaused)
        {
            GameManager.Instance.EndGame(false);
        }
    }

    private void CheckMaxCombo(int newValue)
    {
        if (newValue > _highestCombo)
        {
            _highestCombo = newValue;
        }
    }

    public void ResetLevelPoints()
    {
        _goodNodes.Value = 0;
        _perfectNodes.Value = 0;
        _missedNodes.Value = 0;
        _comboCounter.Value = 0;
        _scoreCounter.Value = 0;
        _momentumCounter.Value = 0;
        _highestCombo = 0;
    }

    public void ResetComboCounter()
    {
        _comboCounter.Value = 0;
    }

    public void ReduceMomentum(float value)
    {
        _momentumCounter.Value -= value;
    }

    private ScoreInfo CreateScore()
    {
        ScoreInfo score = ScriptableObject.CreateInstance("ScoreInfo") as ScoreInfo;
        float hitnodes = _goodNodes.Value + _perfectNodes.Value;
        float missednodes = _missedNodes.Value;
        decimal roundedAccuracy = (decimal)(hitnodes / (missednodes + hitnodes) * 100);
        float accuracy = (float)Decimal.Round(roundedAccuracy, 2);
        score.Init(0, UIManager.Instance.EnteredUserName.text, accuracy, _scoreCounter.Value);

        return score;
    }

    private void GetActiveLevel()
    {
        _currentLevel = GameManager.Instance.ActiveLevel;
    }

    public void AddScoreToLevel()
    {
        GetActiveLevel();

        if (_currentLevel is null)
        {
            Debug.LogError("Level not found!");
            return;
        }

        _currentLevel.ScoreCollection.Add(CreateScore());
        _currentLevel.SortScoreCollection();

        //Saveing all levels to binary file
        SaveGameManager.Instance.SaveLevelInformation(_levelCollection);
    }

    public void CalculateScore(float basePoint)
    {
        _scoreCounter.Value += basePoint * (_momentumCounter + 1) * (_comboCounter + 1);
    }
    #endregion
}
