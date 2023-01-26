using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateSpawner : MonoBehaviour
{
    #region Fields
    [Header("TravelSettings")]
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _rateObjectPrefab;
    [SerializeField] private float _travelTime;

    [Header("SpawnSettings")]
    [SerializeField] private Conductor _conductor;
    private float _rateSpawnTimes;


    [Header("ObjectPool")]
    [SerializeField] private int _poolSize;
    private ObjectPool<RateObject> _pool;
    private GameObject _newButton;
    #endregion

    public ObjectPool<RateObject> Pool => _pool;

    private void Awake()
    {
        _pool = new ObjectPool<RateObject>(_rateObjectPrefab, _poolSize, transform);
    }


    public IEnumerator StartSpawning()
    {
        _rateSpawnTimes = _conductor.BeatPerSec * 4;
        while (!GameManager.Instance.IsPaused)
        {
            _newButton = _pool.GetItem().gameObject;
            _newButton.GetComponent<RateObject>().StartButton(_target, _travelTime);
            yield return new WaitForSecondsRealtime(_rateSpawnTimes);
        }
    }
}
