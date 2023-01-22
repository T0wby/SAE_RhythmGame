using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    [SerializeField, Range(0,7)] private int _band;
    [SerializeField] private float _startScale = 1f;
    [SerializeField] private float _scaleMultiplier = 1f;
    private AudioVisualization _audioVisualization;

    private void Awake()
    {
        _audioVisualization = FindObjectOfType<AudioVisualization>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (_audioVisualization.FreqGroups[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
    }
}
