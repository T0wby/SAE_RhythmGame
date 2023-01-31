using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : SubjectEvent
{
    public void Initialize()
    {
        if(_observers.GetType() == typeof(GameLoop))
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                ((GameLoop)_observers[i]).InvokeInitialize();
            }
        }
        
    }

    public void Update()
    {
        if (_observers.GetType() == typeof(GameLoop))
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                ((GameLoop)_observers[i]).InvokeUpdate();
            }
        }
    }

    //public void Finalize()
    //{
    //    if (_observers.GetType() == typeof(GameLoop))
    //    {
    //        for (int i = _observers.Count - 1; i >= 0; i--)
    //        {
    //            ((GameLoop)_observers[i]).InvokeFinalize();
    //        }
    //    }
    //}
}
