using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionBase<T> : ScriptableObject
{
    #region Properties

    public abstract T Value { get; set; }
    public abstract event System.Action<T> ChangeValue;

    #endregion
}
