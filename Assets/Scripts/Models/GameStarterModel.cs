﻿using System;
using UnityEngine;

[Serializable]
public sealed class GameStarterModel
{
    [SerializeField] private float globalHeightLimit;
    [SerializeField] private float globalGravity = 9.81f;

    [SerializeField] private DragableObjectView[] dragableObjects;
    [SerializeField] private DragSystemInitModel dragSystemInitModel;
    [SerializeField] private ScaleControllerInitModel scaleControllerInitModel;
    [SerializeField] private CameraMoveInitModel cameraMoveInitModel;
    [SerializeField] private SurfacesInitModel surfacesInitModel;

    public DragSystemInitModel DragSystemInitModel => dragSystemInitModel;
    public ScaleControllerInitModel ScaleControllerInitModel => scaleControllerInitModel;
    public CameraMoveInitModel CameraMoveInitModel => cameraMoveInitModel;
    public SurfacesInitModel SurfacesInitModel => surfacesInitModel;
    public DragableObjectView[] DragableObjects => dragableObjects;
    public float GlobalHeightLimit => globalHeightLimit;
    public float GlobalGravity => globalGravity;
}

