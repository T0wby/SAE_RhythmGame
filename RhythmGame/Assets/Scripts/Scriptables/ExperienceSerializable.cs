using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExperienceSerializable
{
    private float _value;

    public float Value { get => _value; set => _value = value; }

    public ExperienceSerializable(float value)
    {
        _value = value;
    }
}
