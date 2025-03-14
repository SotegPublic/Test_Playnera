using UnityEngine;
using UnityEngine.Events;

public sealed class InputModel: IChangebleModel
{
    private Vector2 pointerPosition;
    private Vector2 startPosition;

    public UnityAction<bool> IsOnTouch { get; set; }
    public Vector2 PointerPosition => pointerPosition;
    public Vector2 StartPosition => startPosition;

    void IChangebleModel.ChangePointerPosition(Vector2 position)
    {
        pointerPosition = position;
    }

    void IChangebleModel.ChangeStartPosition(Vector2 position)
    {
        startPosition = position;
    }
}
