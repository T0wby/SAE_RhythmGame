using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Float", menuName = "Scriptable/Variables/Float")]
    public class Float : Condition<float>
    {

        public void OnValueChanged(float newValue)
        {
            Debug.Log($" Float value: {newValue}");
        }

        public static Float operator ++(Float variable)
        {
            variable.Value++;
            variable.InvokeValue();
            return variable;
        }

        public static float operator + (Float lhs, float rhs) => lhs.Value + rhs;
        public static float operator - (Float lhs, float rhs) => lhs.Value - rhs;
        public static implicit operator float (Float variable) => variable.Value;
        public static implicit operator Float (float variable) => variable;

        private void OnEnable() => ChangeValue += OnValueChanged;
        private void OnDisable() => ChangeValue -= OnValueChanged;
    } 
}
