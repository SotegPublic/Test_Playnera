using UnityEngine;

public sealed class SurfaceModel : MonoBehaviour
{
    [SerializeField] private SurfaceType surfaceType;

    public SurfaceType SurfaceType => surfaceType;
}
