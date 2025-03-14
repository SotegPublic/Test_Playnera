using UnityEngine;

public sealed class SurfaceModel : MonoBehaviour
{
    [SerializeField] private Transform[] contactPoints;
    [SerializeField] private SurfaceType surfaceType;

    public SurfaceType SurfaceType => surfaceType;
    public Transform[] ContactPoints => contactPoints;
}
