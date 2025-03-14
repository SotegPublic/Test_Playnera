using System;
using UnityEngine;

[Serializable]
public sealed class SurfacesInitModel
{
    [SerializeField] private float attractionEdge;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private SurfaceModel[] surfaceModels;

    public SurfaceModel[] SurfaceModels => surfaceModels;
    public float AttractionEdge => attractionEdge;
    public float LerpSpeed => lerpSpeed;
}

