using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTowby : MonoBehaviour
{
    private GameObject _target;
    private float _travelTime = 20f;


    public GameObject Target { get => _target; set => _target = value; }


    private void Start()
    {
        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement()
    {
        float time = 0;
        while (_travelTime > time)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, time/_travelTime);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = Target.transform.position;
    }
}
