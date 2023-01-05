using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
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

    private void Start()
    {
        _totalLevelNodes = _spawnerone.SpawnTimings.Count + _spawnertwo.SpawnTimings.Count + _spawnerthree.SpawnTimings.Count + _spawnerfour.SpawnTimings.Count;
        _missedNodes.ChangeValue += CheckLossCondition;
    }


    #region Methods

    private void CheckLossCondition(int newValue)
    {
        if (newValue > (_totalLevelNodes * _lossPercent))
        {
            UIManager.Instance.OpenEndscreen();
        }
    }

    #endregion
}
