using UnityEngine;

public interface IFreeFallModel
{
    public float CurrentFallSpeed { get; set; }
    public bool IsFreeFall { get; }
    public bool IsGoToNearestPoint { get; }
    public Transform ObjectTransform { get; }
    public DragableObjectView ObjectView { get; }

    public void ResetSpeed();
}
