using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using AudioManaging;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    #region Fields
    [Header("TravelSettings")]
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _shortButtonPrefab;
    [SerializeField] private float _travelTime;

    [Header("SpawnSettings")]
    [SerializeField] private float _spawntimer = 2f;
    [SerializeField] private float _startOffsetTimer = 2f;
    [SerializeField] private Conductor _conductor;

    [Header("ObjectPool")]
    [SerializeField] private int _poolSize;
    private ObjectPool<ShortButton> _pool;
    private float _timer;
    private GameObject _newButton;
    private Coroutine _lastCoroutine;
    private List<float> _spawnTimings;
    private int _spawnCount;
    private bool _startspawn = false;
    #endregion

    public ObjectPool<ShortButton> Pool => _pool;

    public List<float> SpawnTimings
    {
        get { return _spawnTimings; }
        set { _spawnTimings = value; }
    }


    private void Awake()
    {
        _pool = new ObjectPool<ShortButton>(_shortButtonPrefab, _poolSize, transform);
        _spawnCount = 0;
    }

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }
    private void Update()
    {
        //if(!_startspawn)
        //    return;

        //if (_conductor.CurrentBeatPos % _spawntimer == 0)
        //{
        //    _newButton = _pool.GetItem().gameObject;
        //    _newButton.GetComponent<AButton>().StartButton(_target, _travelTime);
        //}
    }


    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(_startOffsetTimer);
        StartCoroutine(SpawnButton());
        _startspawn= true;
    }

    private IEnumerator SpawnButton()
    {
        while (true)
        {
            
            _newButton = _pool.GetItem().gameObject;
            _newButton.GetComponent<AButton>().StartButton(_target, _travelTime);
            yield return new WaitForSeconds(_spawntimer);
            _spawnCount++;
        }
    }
}
