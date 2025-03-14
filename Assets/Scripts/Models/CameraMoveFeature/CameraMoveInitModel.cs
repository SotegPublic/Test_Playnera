using System;
using UnityEngine;

[Serializable]
public sealed class CameraMoveInitModel
{
    [SerializeField] private float biasSpeed = 1;
    [SerializeField] private float swipeSpeed = 0.01f;
    [SerializeField] private float edgeLeft = -0.27f;
    [SerializeField] private float edgeRight = 17.27f;
    [SerializeField] private float screenThreshold = 0.15f;

    public float BiasSpeed => biasSpeed;
    public float SwipeSpeed => swipeSpeed;
    public float EdgeLeft => edgeLeft; 
    public float EdgeRight => edgeRight;
    public float ScreenThreshold => screenThreshold;
}