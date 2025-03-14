using System;
using UnityEngine;

[Serializable]
public sealed class ScaleControllerInitModel
{
    [SerializeField] private float scaleModifier = 1.2f;

    public float ScaleModifier => scaleModifier;
}