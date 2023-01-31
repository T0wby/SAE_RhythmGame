using AudioManaging;
using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitLine : MonoBehaviour
{
    #region Fields

    private List<AButton> _buttons;
    private AButton _tmp;
    [SerializeField] private ELineType _lineType;

    //Temp
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;

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

    #endregion
}
