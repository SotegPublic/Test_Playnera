using UnityEngine;

public interface IChangebleModel: IInputModel
{
    void ChangePointerPosition(Vector2 position);
    void ChangeStartPosition(Vector2 position);
}
