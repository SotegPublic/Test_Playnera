using UnityEngine;

public sealed class DragingObjectMoveController: IUpdateble, IGetDragNotifications
{
    private IInputModel inputModel;

    private float cameraDistance;
    private bool isDragObjectOn;
    private GameObject dragObject;
    private Rigidbody2D rigidbody;

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
            rigidbody = dragObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            if(!isDragObjectOn)
                return;

            rigidbody.velocity = Vector2.zero;
            isDragObjectOn = false;
            dragObject = null;
            rigidbody = null;
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
