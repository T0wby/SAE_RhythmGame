using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectEvent : ScriptableObject
{
    #region Fields
    [SerializeField] private List<ObserverListener> _observers= null;
    #endregion

    #region Methods

    public void RegisterObservers(ObserverListener observerListener) => _observers.Add(observerListener);
    public void ReleaseObservers(ObserverListener observerListener) => _observers.Remove(observerListener);

    public void Raise()
    {
        for (int i = _observers.Count - 1; i >= 0 ; i--)
        {
            _observers[i]?.Invoke();
        }
    }

    #endregion
}
