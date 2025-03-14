using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DragNotificationsManager
{
    private List<IGetDragNotifications> observers = new List<IGetDragNotifications>(8);

    public void OnDragActeve(bool isDragObject = false, GameObject view = null)
    {
        for(int i = 0; i < observers.Count; i++)
        {
            observers[i].GetDragNotification(new DragNotification
            {
                DragObject = view,
                IsDragingObject = isDragObject,
                isDragOn = true
            });
        }
    }

    public void OnDragStop()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].GetDragNotification(new DragNotification
            {
                DragObject = null,
                IsDragingObject = false,
                isDragOn = false
            });
        }
    }

    public void Subscribe(IGetDragNotifications observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(IGetDragNotifications observer)
    {
        observers.Remove(observer);
    }
}
