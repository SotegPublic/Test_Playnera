using UnityEngine;

public sealed class DragController: IDisposable
{
    private DragNotificationsManager notificationsManager;

    private IInputModel inputModel;

    private LayerMask layerMask;
    private float cameraDistance;
    private bool isDragOn;

    public DragNotificationsManager DragNotificationsManager => notificationsManager;

    public DragController(IInputModel playerInputModel, DragSystemInitModel initModel, DragNotificationsManager dragNotificationsManager)
    {
        inputModel = playerInputModel;
        layerMask = initModel.Mask;

        notificationsManager = dragNotificationsManager;

        cameraDistance = Mathf.Abs(Camera.main.transform.position.z);

        inputModel.IsOnTouch += WhenTouchActionInvoke;
    }

    private void WhenTouchActionInvoke(bool isTouchActive)
    {
        if (isTouchActive)
        {
            var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputModel.StartPosition.x, inputModel.StartPosition.y, cameraDistance));

            var hit = Physics2D.Raycast(new Vector2(worldStartPosition.x, worldStartPosition.y), Vector2.zero, cameraDistance, layerMask);

            var str = hit.collider == null ? "null" : hit.collider.gameObject.name;
            Debug.Log(str);

            if (hit.collider != null)
            {
                notificationsManager.OnDragActeve(true, hit.collider.gameObject);
            }
            else
            {
                notificationsManager.OnDragActeve();
            }

            isDragOn = true;
        }
        else
        {
            if (!isDragOn)
                return;
            notificationsManager.OnDragStop();
            isDragOn = false;
        }
    }

    public void Dispose()
    {
        inputModel.IsOnTouch -= WhenTouchActionInvoke;
        notificationsManager.OnDragStop();
        isDragOn = false;
    }
}
