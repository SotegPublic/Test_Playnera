using UnityEngine;

public sealed class ScaleController : IGetDragNotifications
{

    private float scaleModifier;
    private Vector3 baseScale;
    private GameObject currentObject;
    private bool isDragOn;

    public ScaleController(ScaleControllerInitModel model)
    {
        scaleModifier = model.ScaleModifier;
    }
    
    public void GetDragNotification(DragNotification notification)
    {
        if(notification.isDragOn)
        {
            if (!notification.IsDragingObject)
                return;

            currentObject = notification.DragObject;
            baseScale = currentObject.transform.localScale;
            currentObject.transform.localScale = baseScale * scaleModifier;
            isDragOn = true;
        }
        else
        {
            if(!isDragOn) 
                return;

            isDragOn = false;
            currentObject.transform.localScale = baseScale;
            currentObject = null;
        }

    }
}
