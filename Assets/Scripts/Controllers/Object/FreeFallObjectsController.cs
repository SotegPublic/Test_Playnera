using UnityEngine;

public sealed class FreeFallObjectsController : IUpdateble
{
    private float heightLimit;
    private float gravity;
    private IFreeFallModel[] objectModelss;

    public FreeFallObjectsController(ObjectModel[] dragableObjectModels, float globalHeightLimit, float globalGravity)
    {
        gravity = globalGravity;
        heightLimit = globalHeightLimit;
        objectModelss = dragableObjectModels;
    }

    public void LocalUpdate()
    {
        for(int i = 0; i < objectModelss.Length;i++)
        {
            if (objectModelss[i].IsGoToNearestPoint)
                return;

            if (objectModelss[i].IsFreeFall)
            {
                objectModelss[i].CurrentFallSpeed -= gravity * Time.deltaTime;

                objectModelss[i].ObjectTransform.Translate(0, objectModelss[i].CurrentFallSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (objectModelss[i].CurrentFallSpeed != 0)
                    objectModelss[i].ResetSpeed();
            }

            if (objectModelss[i].ObjectTransform.position.y <= heightLimit)
            {
                objectModelss[i].ObjectView.SetIsFreeFall(false);
                objectModelss[i].ResetSpeed();
            }
        }
    }
}
