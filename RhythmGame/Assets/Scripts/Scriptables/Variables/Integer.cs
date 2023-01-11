using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Integer", menuName = "Scriptable/Variables/Integer")]
    public class Integer : Condition<int>
    {

        public static Integer operator ++(Integer variable)
        {
            variable.Value++;
            variable.InvokeValue();
            return variable;
        }

        public static int operator +(Integer lhs, int rhs) => lhs.Value + rhs;
        public static int operator -(Integer lhs, int rhs) => lhs.Value - rhs;
        public static implicit operator int(Integer variable) => variable.Value;
        public static implicit operator Integer(int variable) => variable;
    }
}

