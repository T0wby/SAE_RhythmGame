using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateObject : MonoBehaviour, IPoolable<RateObject>
{
    private GameObject _targetOne;
    private float _travelTime = 3f;
    private ObjectPool<RateObject> _pool;

    public GameObject TargetOne { get => _targetOne; set => _targetOne = value; }
    public float TravelTime { get => _travelTime; set => _travelTime = value; }
    public EButtonType Type { get; set; }

    public void StartButton(GameObject targetOne, float travelTime)
    {
        _targetOne = targetOne;
        _travelTime = travelTime;
        StartCoroutine(StartMovementOne());
    }

    public void Deactivate()
    {
        gameObject?.SetActive(false);
    }

    public void Initialize(ObjectPool<RateObject> pool)
    {
        _pool = pool;
    }

    public void Reset()
    {
        gameObject?.SetActive(true);
    }

    private IEnumerator StartMovementOne()
    {
        float time = 0;
        float currProg;
        while (time <= _travelTime)
        {
            currProg = time / _travelTime;
            transform.position = Vector3.Lerp(transform.parent.position, _targetOne.transform.position, currProg);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        transform.position = _targetOne.transform.position;
    }
}
