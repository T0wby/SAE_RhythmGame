using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerTowby : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _spawntimer = 2f;
    [SerializeField] private float _startOffsetTimer = 2f;
    [SerializeField] private GameObject _shortButton;
    [SerializeField] private float _travelTime;
    private float _timer;
    private GameObject _newButton;
    private Coroutine _lastCoroutine;


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
            _newButton = Instantiate(_shortButton, transform.position, Quaternion.identity);
            //TODO: Set button type to later identify it on trigger enter/stay
            _newButton.GetComponent<AButton>().StartButton(_target, _travelTime);
            yield return new WaitForSeconds(_spawntimer);
        }
    }
}
