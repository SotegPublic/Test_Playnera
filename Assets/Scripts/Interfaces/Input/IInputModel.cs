using UnityEngine;
using UnityEngine.Events;

public interface IInputModel
{
    public UnityAction<bool> IsOnTouch { get; set; }
    public Vector2 PointerPosition { get; }
    public Vector2 StartPosition { get; }
}
