using AudioManaging;
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
        float currProg = 0;
        while (time <=_travelTime )
        {
            currProg = time / _travelTime;
            transform.position = Vector3.Lerp(transform.parent.position, Target.transform.position, currProg);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        transform.position = Target.transform.position;
    }
}
