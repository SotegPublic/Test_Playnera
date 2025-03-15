using UnityEngine;

public interface IStickingModel
{
    public Vector2 NearestPoint { get; set; }
    public Vector2 StartPoint { get; set; }
    public float Progress { get; set; }

    public bool IsFreeFall { get; }
    public bool IsGoToNearestPoint { get; }
    public DragableObjectView ObjectView { get; }
    public Transform ObjectTransform { get; }
}
