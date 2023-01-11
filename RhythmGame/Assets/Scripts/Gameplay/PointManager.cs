using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : Singleton<PointManager>
{
    [Header("Level")]
    [SerializeField] private LevelInfo[] _levelCollection = null;

    [Header("Points")]
    [SerializeField] private Integer _goodNodes;
    [SerializeField] private Integer _perfectNodes;
    [SerializeField] private Integer _missedNodes;
    [SerializeField] private Integer _comboCounter;
    [SerializeField, Range(0 , 1)] private float _lossPercent;
    private int _totalLevelNodes = 0;

    [Header("Spawner")]
    [SerializeField] private Spawner _spawnerone;
    [SerializeField] private Spawner _spawnertwo;
    [SerializeField] private Spawner _spawnerthree;
    [SerializeField] private Spawner _spawnerfour;

    public Integer GoodNodes { get => _goodNodes; }
    public Integer PerfectNodes { get => _perfectNodes; }
    public Integer MissedNodes { get => _missedNodes; }
    public Integer ComboCounter { get => _comboCounter; set => _comboCounter = value; }


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
        ResetLevelPoints();
    }
    #endregion



    #region Methods

    private void CheckLossCondition(int newValue)
    {
        if (newValue > (_totalLevelNodes * _lossPercent))
        {
            UIManager.Instance.OpenEndscreen();
        }
    }


    public void ResetLevelPoints()
    {
        _goodNodes.Value = 0;
        _perfectNodes.Value = 0;
        _missedNodes.Value = 0;
        _comboCounter.Value = 0;
    }

    private ScoreInfo CreateScore()
    {
        ScoreInfo score = ScriptableObject.CreateInstance("ScoreInfo") as ScoreInfo;
        /**TODO: Add Score **/
        float hitnodes = _goodNodes.Value + _perfectNodes.Value;
        float missednodes = _missedNodes.Value;
        float accuracy = (hitnodes / (missednodes + hitnodes));
        score.Init(0, UIManager.Instance.EnteredUserName.text, accuracy, 0);

        return score;
    }

    public void AddScoreToLevel()
    {
        LevelInfo level = (LevelInfo)Resources.Load($"LevelInfos/{GameManager.Instance.ActiveLevel}");

        if (level is null)
        {
            Debug.LogError("Level not found!");
            return;
        }

        level.ScoreCollection.Add(CreateScore());
        level.SortScoreCollection();

        //Saveing all levels to binary file
        SaveGameManager.Instance.SaveLevelInformation(_levelCollection);
    }
    #endregion
}
