using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateLine : MonoBehaviour
{
    [SerializeField] private RateSpawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        RateObject tmp = other.GetComponent<RateObject>();
        if (tmp != null)
        {
            _spawner.Pool.ReturnItem(tmp);
        }
    }
}
