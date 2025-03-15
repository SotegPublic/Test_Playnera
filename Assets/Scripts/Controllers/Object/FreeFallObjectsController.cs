using UnityEngine;

public class FreeFallObjectsController : IUpdateble
{
    private float heightLimit;
    private float gravity = 9.81f;
    private ObjectModel[] objects;

    public FreeFallObjectsController(DragableObjectController[] objectControllers, float globalHeightLimit)
    {
        heightLimit = globalHeightLimit;
        objects = new ObjectModel[objectControllers.Length];

        for(int i = 0; i < objectControllers.Length; i++)
        {
            objects[i] = new ObjectModel(objectControllers[i]);
        }
    }

    public void LocalUpdate()
    {
        for(int i = 0; i < objects.Length;i++)
        {
            if (objects[i].IsGoToNearestPoint)
                return;

            if (objects[i].IsFreeFall)
            {
                objects[i].CurrentSpeed -= gravity * Time.deltaTime;

                objects[i].ObjectTransform.Translate(0, objects[i].CurrentSpeed * Time.deltaTime, 0);
            }

            if (objects[i].ObjectTransform.position.y <= heightLimit)
            {
                objects[i].ObjectController.SetIsFreeFall(false);
                objects[i].ResetSpeed();

            }
        }
    }
}

public class ObjectModel
{
    public float CurrentSpeed;

    private Transform objectTransform;
    private DragableObjectController objectController;

    public bool IsFreeFall => objectController.IsFreeFall;
    public bool IsGoToNearestPoint => objectController.IsGoToNearestPoint;
    public Transform ObjectTransform => objectTransform;
    public DragableObjectController ObjectController => objectController;

    public ObjectModel(DragableObjectController dragableObjectController)
    {
        objectController = dragableObjectController;
        objectTransform = objectController.gameObject.transform;
    }

    public void ResetSpeed()
    {
        CurrentSpeed = 0;
    }
}
