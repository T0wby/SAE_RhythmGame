using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using AudioManaging;
using UnityEngine;

public class SpawnerTowby : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _spawntimer = 2f;
    [SerializeField] private float _startOffsetTimer = 2f;
    [SerializeField] private GameObject _shortButtonPrefab;
    [SerializeField] private float _travelTime;
    [SerializeField] private int _poolSize;
    private ObjectPool<ShortButton> _pool;
    private float _timer;
    private GameObject _newButton;
    private Coroutine _lastCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<ShortButton>(_shortButtonPrefab, _poolSize, transform);
    }

    void Update()
    {
        if (_timer < _startOffsetTimer)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            if (_lastCoroutine == null)
            {
                _lastCoroutine = StartCoroutine(SpawnButton());
            }
        }
    }

    IEnumerator SpawnButton()
    {
        while (true)
        {
            _newButton = _pool.GetItem().gameObject;
            _newButton.GetComponent<AButton>().StartButton(_target, _travelTime);
            yield return new WaitForSeconds(_spawntimer);
        }
    }
}
