using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : Singleton<PointManager>
{
    [Header("Points")]
    [SerializeField] private Integer _goodNodes;
    [SerializeField] private Integer _perfectNodes;
    [SerializeField] private Integer _missedNodes;
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
    }

    private ScoreInfo CreateScore()
    {
        ScoreInfo score = ScriptableObject.CreateInstance("ScoreInfo") as ScoreInfo;
        /**TODO: Add points etc.**/
        score.Init(0, UIManager.Instance.EnteredUserName.text, 0, 0);

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

        /**TODO: Some kind of sorting for all scores**/
        level.ScoreCollection.Add(CreateScore());
        Debug.Log($"ScoreCount in {GameManager.Instance.ActiveLevel}: {level.ScoreCollection.Count}");

        //Saveing all levels to binary file
        SaveGameManager.Instance.SaveLevelInformation();
    }
    #endregion
}
