using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AButton : MonoBehaviour
{
    protected GameObject _target;
    protected float _travelTime = 20f;
    protected EButtonType _type = EButtonType.NONE;

    public GameObject Target { get => _target; set => _target = value; }
    public float TravelTime { get => _travelTime; set => _travelTime = value; }
    public EButtonType Type { get; set; }


    public void StartButton(GameObject target, float travelTime)
    {
        _target = target;
        _travelTime = travelTime;
        StartCoroutine(StartMovement());
    }

    protected IEnumerator StartMovement()
    {
        float time = 0;
        while (_travelTime > time)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, time / _travelTime);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = Target.transform.position;
    }
}
