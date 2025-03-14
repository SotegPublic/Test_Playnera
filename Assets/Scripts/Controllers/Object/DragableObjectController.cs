using System;
using UnityEngine;

public sealed class DragableObjectController : MonoBehaviour, IDragableObject, IUpdateble
{
    [SerializeField] private LayerMask solidLayers;
    [SerializeField] private Rigidbody2D rb2d;

    private SurfaceModel[] surfaces;
    private bool isFreeFall = true;
    private float heightLimit;

    private bool isGoToNearestPoint;
    private Vector2 nearestPoint;
    private Vector2 startPoint;
    private float progress;


    private float attractionEdgeSquared;
    private float lerpToPointSpeed;

    public void InjectParameters(SurfacesInitModel model, float globalHeightLimit)
    {
        attractionEdgeSquared = model.AttractionEdge * model.AttractionEdge;
        lerpToPointSpeed = model.LerpSpeed;
        heightLimit = globalHeightLimit;

        surfaces = new SurfaceModel[model.SurfaceModels.Length];

        for(int i = 0; i < model.SurfaceModels.Length; i++)
        {
            surfaces[i] = model.SurfaceModels[i];
        }
    }

    public void SetIsFreeFall(bool flag)
    {
        isFreeFall = flag;
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0f;
        rb2d.isKinematic = isFreeFall? false: true;

        if(!flag && isGoToNearestPoint)
        {
            isGoToNearestPoint = false;
            progress = 0f;
        }
    }

    public void LocalUpdate()
    {
        if (isFreeFall)
        {
            if (TryFindNearContactPoint(out var point))
            {
                SetIsFreeFall(false);
                nearestPoint = point;
                startPoint = transform.position;
                isGoToNearestPoint = true;
                return;
            }

            if(transform.position.y <= heightLimit)
            {
                SetIsFreeFall(false);
            }
        }

        if(isGoToNearestPoint)
        {
            LerpToPoint();
        }
    }

    private void LerpToPoint()
    {
        progress += Time.deltaTime * lerpToPointSpeed;

        transform.position = Vector2.Lerp(startPoint, nearestPoint, progress);

        if(progress >= 1)
        {
            isGoToNearestPoint = false;
            progress = 0;
        }
    }

    private bool TryFindNearContactPoint(out Vector2 point)
    {
        Vector2 nearestPoint = Vector2.zero;
        float nearestDistance = 1000f;

        Vector2 position2d = transform.position;

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
