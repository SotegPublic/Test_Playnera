using System;
using UnityEngine;

public sealed class DragableObjectView : MonoBehaviour, IDragableObject
{
    private bool isFreeFall = true;
    private bool isGoToNearestPoint;

    public bool IsFreeFall => isFreeFall;
    public bool IsGoToNearestPoint => isGoToNearestPoint;

    public void SetIsFreeFall(bool flag)
    {
        isFreeFall = flag;
        if(!flag && isGoToNearestPoint)
        {
            isGoToNearestPoint = false;
        }
    }

    public void SetIsGoToNearestPoint(bool flag)
    {
        isGoToNearestPoint = flag;
    }
}
