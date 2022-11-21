using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObserverListener : MonoBehaviour
{
    #region Fields

    [SerializeField] private SubjectEvent _subject = null;
    [SerializeField] private UnityEvent _response = null;

    #endregion

    #region Methods

    public void Invoke() => _response?.Invoke();

    #region Unity

    private void Awake()
    {
        _response ??= new UnityEvent();
        _subject ??= FindObjectOfType<SubjectEvent>();
    }

    private void OnEnable()
    {
        _subject?.RegisterObservers(this);
    }

    private void OnDisable()
    {
        _subject?.ReleaseObservers(this);
    }

    #endregion

    #endregion
}
