using System;
using UnityEngine;

[Serializable]
public sealed class GameStarterModel
{
    [SerializeField] private DragSystemInitModel dragSystemInitModel;
    [SerializeField] private ScaleControllerInitModel scaleControllerInitModel;
    [SerializeField] private CameraMoveInitModel cameraMoveInitModel;

    public DragSystemInitModel DragSystemInitModel => dragSystemInitModel;
    public ScaleControllerInitModel ScaleControllerInitModel => scaleControllerInitModel;
    public CameraMoveInitModel CameraMoveInitModel => cameraMoveInitModel;
}

public class SurfacesModels
{
    [SerializeField] private SurfaceModel[] surfaceModels;

    public SurfaceModel[] SurfaceModels => surfaceModels;
}
