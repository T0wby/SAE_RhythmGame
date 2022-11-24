using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    #region Fields

    [SerializeField] private Spawner _spawner;
    private List<AButton> _buttons;
    private AButton _tmp;

    #endregion

    #region Properties

    public List<AButton> Buttons => _buttons;

    #endregion

    #region Methods

    #region Unity

    private void Start()
    {
        _buttons = new List<AButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _tmp = other.gameObject.GetComponent<AButton>();
        if (_tmp is not null)
        {
            _buttons.Add(_tmp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _tmp = other.gameObject.GetComponent<AButton>();
        if (_tmp is not null)
        {
            _buttons.Remove(_tmp);
        }
    }

    #endregion

    public void ResetArea()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            //_buttons[i].gameObject.SetActive(false);
            if (_buttons[i].GetType() == typeof(ShortButton))
            {
                _spawner.Pool.ReturnItem((ShortButton)_buttons[i]);
            }
        }
        _buttons.Clear();
    }

    #endregion




}
