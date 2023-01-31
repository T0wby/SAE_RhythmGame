using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AButton : MonoBehaviour, IPoolable<AButton>
{
    protected GameObject _targetOne;
    protected GameObject _targetTwo;
    protected float _travelTime = 3f;
    protected float _travelTimeExtra = 3f;
    protected EButtonType _type = EButtonType.NONE;

    public GameObject TargetOne { get => _targetOne; set => _targetOne = value; }
    public GameObject TargeTwo { get => _targetTwo; set => _targetTwo = value; }
    public float TravelTime { get => _travelTime; set => _travelTime = value; }
    public EButtonType Type { get; set; }

    public virtual void Deactivate()
    {
        gameObject?.SetActive(false);
    }

    public virtual void Initialize(ObjectPool<AButton> pool)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Reset()
    {
        gameObject?.SetActive(true);
    }

    public void StartButton(GameObject targetOne, GameObject targetTwo, float travelTime)
    {
        _targetOne = targetOne;
        _targetTwo = targetTwo;
        _travelTime = travelTime;
        _travelTimeExtra = travelTime * 0.1f;
        StartCoroutine(StartMovementOne());
    }

    protected IEnumerator StartMovementOne()
    {
        float time = 0;
        float currProg;
        while (time <=_travelTime )
        {
            currProg = time / _travelTime;
            transform.position = Vector3.Lerp(transform.parent.position, _targetOne.transform.position, currProg);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        transform.position = _targetOne.transform.position;
        StartCoroutine(StartMovementTwo());
    }

    protected IEnumerator StartMovementTwo()
    {
        float time = 0;
        float currProg;
        while (time <= _travelTimeExtra)
        {
            currProg = time / _travelTimeExtra;
            transform.position = Vector3.Lerp(TargetOne.transform.position, _targetTwo.transform.position, currProg);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        transform.position = _targetTwo.transform.position;
    }
}
