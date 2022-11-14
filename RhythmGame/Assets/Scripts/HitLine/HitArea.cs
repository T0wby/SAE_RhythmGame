using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    #region Fields

    [Range(1, 4)]
    [SerializeField] private int _lineIndex = 1;
    private List<AButton> _buttons;
    private AButton _tmp;

    #endregion

    #region Properties

    public int LineIndex => _lineIndex;
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

    #endregion




}
