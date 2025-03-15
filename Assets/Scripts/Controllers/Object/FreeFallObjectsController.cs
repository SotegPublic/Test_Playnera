using UnityEngine;

public sealed class FreeFallObjectsController : IUpdateble
{
    private float heightLimit;
    private float gravity = 9.81f;
    private IFreeFallModel[] objectModelss;

    public FreeFallObjectsController(ObjectModel[] dragableObjectModels, float globalHeightLimit)
    {
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

            if (objectModelss[i].ObjectTransform.position.y <= heightLimit)
            {
                objectModelss[i].ObjectView.SetIsFreeFall(false);
                objectModelss[i].ResetSpeed();

            }
        }
    }
}
