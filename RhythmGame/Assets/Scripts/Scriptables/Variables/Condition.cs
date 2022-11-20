using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition<T> : ConditionBase<T>
{
    #region Fields
    [SerializeField] private T _value;

    #endregion

    #region Properties
    public override T Value
    {
        get { return _value; }
        set 
        { 
            _value = value;
            Invoke();
        }
    }
    #endregion

    public override event Action<T> ChangeValue = (context) => { };

    public void Invoke() => ChangeValue.Invoke(_value);

    private void OnValidate() => Invoke();

}
