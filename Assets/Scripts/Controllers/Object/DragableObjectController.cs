using System;
using UnityEngine;

public sealed class DragableObjectController : MonoBehaviour, IDragableObject, IUpdateble
{
    [SerializeField] private LayerMask solidLayers;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Collider2D collider2d;

    private SurfaceModel[] surfaces;
    private bool isFreeFall = true;

    private bool isGoToNearestPoint;
    private Vector2 nearestPoint;
    private Vector2 startPoint;
    private float progress;

    private Collider2D[] contactPoints = new Collider2D[16];

    private float attractionEdgeSquared;
    private float lerpToPointSpeed;

    public void InjectSurfaces(SurfaceModel[] surfaceModels, float attractionEdge, float lerpSpeed)
    {
        attractionEdgeSquared = attractionEdge * attractionEdge;
        lerpToPointSpeed = lerpSpeed;

        surfaces = new SurfaceModel[surfaceModels.Length];

        for(int i = 0; i < surfaceModels.Length; i++)
        {
            surfaces[i] = surfaceModels[i];
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isFreeFall)
        {
            var contactsCount = collider2d.GetContacts(contactPoints);

            for(int i = 0; i < contactsCount; i++)
            {
                if (((1 << contactPoints[i].gameObject.layer) & solidLayers) != 0)
                {
                    SetIsFreeFall(false);
                }
            }
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
