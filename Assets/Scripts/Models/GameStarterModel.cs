using System;
using UnityEngine;

[Serializable]
public sealed class GameStarterModel
{
    [SerializeField] private float globalHeightLimit;

    [SerializeField] private DragableObjectController[] dragableObjects;
    [SerializeField] private DragSystemInitModel dragSystemInitModel;
    [SerializeField] private ScaleControllerInitModel scaleControllerInitModel;
    [SerializeField] private CameraMoveInitModel cameraMoveInitModel;
    [SerializeField] private SurfacesInitModel surfacesInitModel;

    public DragSystemInitModel DragSystemInitModel => dragSystemInitModel;
    public ScaleControllerInitModel ScaleControllerInitModel => scaleControllerInitModel;
    public CameraMoveInitModel CameraMoveInitModel => cameraMoveInitModel;
    public SurfacesInitModel SurfacesInitModel => surfacesInitModel;
    public DragableObjectController[] DragableObjects => dragableObjects;
    public float GlobalHeightLimit => globalHeightLimit;
}

