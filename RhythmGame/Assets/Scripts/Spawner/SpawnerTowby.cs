using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerTowby : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _spawntimer = 2f;
    [SerializeField] private float _startOffsetTimer = 2f;
    [SerializeField] private GameObject _button;
    private GameObject _newButton;
    private float _timer;
    private Coroutine _lastCoroutine;


    // Update is called once per frame
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
            _newButton = Instantiate(_button, transform.position, Quaternion.identity);
            _newButton.GetComponent<ButtonTowby>().Target = _target;
            yield return new WaitForSeconds(_spawntimer);
        }
    }
}
