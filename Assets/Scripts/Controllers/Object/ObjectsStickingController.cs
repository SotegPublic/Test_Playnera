using UnityEngine;

public sealed class ObjectsStickingController: IUpdateble
{
    private IStickingModel[] objectModels;
    private SurfaceModel[] surfaces;

    private float attractionEdgeSquared;
    private float lerpToPointSpeed;

    public ObjectsStickingController(ObjectModel[] dragableObjectModels, SurfacesInitModel model)
    {
        objectModels = dragableObjectModels;

        attractionEdgeSquared = model.AttractionEdge * model.AttractionEdge;
        lerpToPointSpeed = model.LerpSpeed;

        surfaces = new SurfaceModel[model.SurfaceModels.Length];

        for (int i = 0; i < model.SurfaceModels.Length; i++)
        {
            surfaces[i] = model.SurfaceModels[i];
        }
    }

    public void LocalUpdate()
    {
        for (int i = 0; i < objectModels.Length; i++)
        {
            if (objectModels[i].IsFreeFall)
            {
                if (TryFindNearContactPoint(objectModels[i].ObjectTransform, out var point))
                {
                    objectModels[i].ObjectView.SetIsFreeFall(false);
                    objectModels[i].NearestPoint = point;
                    objectModels[i].StartPoint = objectModels[i].ObjectTransform.position;
                    objectModels[i].ObjectView.SetIsGoToNearestPoint(true);
                }
            }


            if (objectModels[i].IsGoToNearestPoint)
            {
                LerpToPoint(objectModels[i]);
            }
        }
    }

    private void LerpToPoint(IStickingModel objectModel)
    {
        objectModel.Progress += Time.deltaTime * lerpToPointSpeed;

        objectModel.ObjectTransform.position = Vector2.Lerp(objectModel.StartPoint, objectModel.NearestPoint, objectModel.Progress);

        if (objectModel.Progress >= 1)
        {
            objectModel.ObjectView.SetIsGoToNearestPoint(false);
            objectModel.Progress = 0;
        }
    }

    private bool TryFindNearContactPoint(Transform objecTransform, out Vector2 point)
    {
        Vector2 nearestPoint = Vector2.zero;
        float nearestDistance = 1000f;

        Vector2 position2d = objecTransform.position;

        for (int i = 0; i < surfaces.Length; i++)
        {
            if (surfaces[i].ContactPoints.Length == 0)
                continue;

            if (surfaces[i].SurfaceType == SurfaceType.Shelf || surfaces[i].SurfaceType == SurfaceType.Table)
            {
                var contactPoints = surfaces[i].ContactPoints;

                for (int j = 0; j < contactPoints.Length; j++)
                {
                    Vector2 pointPosition2d = contactPoints[j].position;
                    var sqrDistance = (pointPosition2d - position2d).sqrMagnitude;

                    if (sqrDistance <= attractionEdgeSquared)
                    {
                        if (sqrDistance < nearestDistance)
                        {
                            nearestDistance = sqrDistance;
                            nearestPoint = pointPosition2d;
                        }
                    }
                }
            }
        }

        point = nearestPoint;
        return nearestPoint != Vector2.zero;
    }
}
