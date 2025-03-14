using System;
using UnityEngine;

public sealed class CameraMoveController : IGetDragNotifications, ILateUpdateble
{
    private IInputModel inputModel;
    private CameraMoveType moveType;
    private bool isDragOn;
    private Transform cameraTransform;
    private Vector3 startPosition;

    private float biasSpeed;
    private float swipeSpeed;
    private float edgeLeft;
    private float edgeRight;
    private float screenThreshold;

    public CameraMoveController(IInputModel playerInputModel, CameraMoveInitModel model)
    {
        inputModel = playerInputModel;
        cameraTransform = Camera.main.transform;

        biasSpeed = model.BiasSpeed;
        swipeSpeed = model.SwipeSpeed;
        edgeLeft = model.EdgeLeft;
        edgeRight = model.EdgeRight;
        screenThreshold = model.ScreenThreshold;
    }

    public void GetDragNotification(DragNotification notification)
    {
        if(notification.isDragOn)
        {
            moveType = notification.IsDragingObject ? CameraMoveType.Bias : CameraMoveType.Drag;
            startPosition = cameraTransform.position;
            isDragOn = true;
        }
        else
        {
            isDragOn = false;
        }
    }

    public void LocalLateUpdate()
    {
        if (!isDragOn)
            return;

        switch (moveType)
        {
            case CameraMoveType.Drag:
                DragMove();
                break;
            case CameraMoveType.Bias:
                BiasMove();
                break;
            default:
            case CameraMoveType.None:
                break;
        }
    }

    private void BiasMove()
    {
        float edgeThresholdX = Screen.width * screenThreshold;

        var direction = Vector3.zero;
        if(inputModel.PointerPosition.x <= edgeThresholdX)
        {
            direction.x = -1 * biasSpeed * Time.deltaTime;
        }
        else if(inputModel.PointerPosition.x >= Screen.width - edgeThresholdX)
        {
            direction.x = 1 * biasSpeed * Time.deltaTime;
        }

        var newPosition = cameraTransform.position + direction;

        SetNewCameraPosition(newPosition);
    }

    private void DragMove()
    {
        var startX = inputModel.StartPosition.x;
        var currentX = inputModel.PointerPosition.x;

        var delta = currentX - startX;
        var newPosition = cameraTransform.position;
        newPosition.x = startPosition.x - delta * swipeSpeed;

        SetNewCameraPosition(newPosition);
    }

    private void SetNewCameraPosition(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, edgeLeft, edgeRight);
        cameraTransform.position = newPosition;
    }
}
