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
    private float _time;
    private bool _allowedToSpawn = false;


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

    private void Start()
    {
        _rateSpawnTimes = _conductor.BeatPerSec * 4;
        _travelTime = GameManager.Instance.TravelTime;
        _time = _rateSpawnTimes;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused && _allowedToSpawn)
        {
            if (_time <= _rateSpawnTimes)
            {
                _time += Time.deltaTime;
            }
            else
            {
                _time = 0;
                _newButton = _pool.GetItem().gameObject;
                _newButton.GetComponent<RateObject>().StartButton(_target, _travelTime);
            }
        }
    }

    //public IEnumerator StartSpawning()
    //{
    //    while (true)
    //    {
    //        if (!GameManager.Instance.IsPaused)
    //        {
    //            _newButton = _pool.GetItem().gameObject;
    //            _newButton.GetComponent<RateObject>().StartButton(_target, _travelTime);
    //            yield return new WaitForSecondsRealtime(_rateSpawnTimes);
    //        }
    //    }
    //}
    public void StartSpawning()
    {
        _allowedToSpawn = true;
    }
}
