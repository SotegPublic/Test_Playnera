using System;
using UnityEngine;

[Serializable]
public sealed class DragSystemInitModel
{
    [SerializeField] private LayerMask mask;

    public LayerMask Mask => mask;
}
