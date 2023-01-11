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
    [SerializeField] private Conductor _conductor;
    [SerializeField] private List<float> _spawnTimings;


    [Header("ObjectPool")]
    [SerializeField] private int _poolSize;
    private ObjectPool<ShortButton> _pool;
    private GameObject _newButton;
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
    }

    private void Update()
    {
        if (_spawnTimings.Count == 0)
            return;

        if (_conductor.CurrentSongPos >= _spawnTimings[0] && !GameManager.Instance.IsPaused)
        {
            _newButton = _pool.GetItem().gameObject;
            _newButton.GetComponent<AButton>().StartButton(_target, _travelTime);
            _spawnTimings.RemoveAt(0);
        }
    }
}
