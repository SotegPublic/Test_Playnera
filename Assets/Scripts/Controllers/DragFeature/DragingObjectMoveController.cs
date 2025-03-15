using UnityEngine;

public sealed class DragingObjectMoveController: IUpdateble, IGetDragNotifications
{
    private IInputModel inputModel;

    private float cameraDistance;
    private bool isDragObjectOn;
    private GameObject dragObject;

    public DragingObjectMoveController(IInputModel playerInputModel)
    {
        inputModel = playerInputModel;

        cameraDistance = Mathf.Abs(Camera.main.transform.position.z);
    }

    public void GetDragNotification(DragNotification notification)
    {
        if(notification.isDragOn)
        {
            if (!notification.IsDragingObject)
                return;

            isDragObjectOn = true;
            dragObject = notification.DragObject;
            
            if(dragObject.TryGetComponent<IDragableObject>(out var component))
            {
                component.SetIsFreeFall(false);
            }
        }
        else
        {
            if(!isDragObjectOn)
                return;

            isDragObjectOn = false;

            if (dragObject.TryGetComponent<IDragableObject>(out var component))
            {
                component.SetIsFreeFall(true);
            }

            dragObject = null;
        }
    }

    public void LocalUpdate()
    {
        if(!isDragObjectOn)
            return;
        
        var pointerWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputModel.PointerPosition.x, inputModel.PointerPosition.y, cameraDistance));
        dragObject.transform.position = new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, dragObject.transform.position.z);
    }
}
