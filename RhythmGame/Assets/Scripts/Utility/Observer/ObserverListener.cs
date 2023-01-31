using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObserverListener : MonoBehaviour
{
    #region Fields

    [SerializeField] protected SubjectEvent _subject = null;
    [SerializeField] private UnityEvent _response = null;

    #endregion

    #region Methods

    public void Invoke() => _response?.Invoke();

    #region Unity

    protected virtual void Awake()
    {
        _response ??= new UnityEvent();
        _subject ??= FindObjectOfType<SubjectEvent>();
    }

    protected virtual void OnEnable()
    {
        _subject?.RegisterObservers(this);
    }

    protected virtual void OnDisable()
    {
        _subject?.ReleaseObservers(this);
    }

    #endregion

    #endregion
}
