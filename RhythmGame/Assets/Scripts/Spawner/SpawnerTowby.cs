using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerTowby : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _spawntimer = 2f;
    [SerializeField] private GameObject _button;
    private GameObject _newButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnButton());
    }

    // Update is called once per frame
    void Update()
    {
        
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
