using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition<T> : ConditionBase<T> where T : IEquatable<T>
{
    #region Fields
    [SerializeField] private T _value;
    private event Action<T> _changeValue = (context) => { };

    #endregion

    #region Properties
    public override T Value
    {
        get { return _value; }
        set 
        {
            if (!_value.Equals(value))
            {
                _value = value;
                _changeValue.Invoke(_value);
            }
        }
    }
    #endregion

    public override event Action<T> ChangeValue { add => _changeValue += value; remove => _changeValue -= value; }

    public void InvokeValue() => _changeValue.Invoke(_value);

    private void OnValidate() => InvokeValue();

}
