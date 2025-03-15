using UnityEngine;

public class ObjectModel: IStickingModel, IFreeFallModel
{
    public Vector2 NearestPoint { get; set; }
    public Vector2 StartPoint { get; set; }
    public float Progress { get; set; }
    public float CurrentFallSpeed { get; set; }

    private Transform objectTransform;
    private DragableObjectView objectView;

    public bool IsFreeFall => objectView.IsFreeFall;
    public bool IsGoToNearestPoint => objectView.IsGoToNearestPoint;
    public Transform ObjectTransform => objectTransform;
    public DragableObjectView ObjectView => objectView;

    public ObjectModel(DragableObjectView dragableObjectController)
    {
        objectView = dragableObjectController;
        objectTransform = objectView.gameObject.transform;
    }

    public void ResetSpeed()
    {
        CurrentFallSpeed = 0;
    }
}