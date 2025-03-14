using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public sealed class UserInputController: IDisposable, IUpdateble
{
    private IChangebleModel model;
    private PlayerInput playerInput;
    
    public IInputModel InputModel => model;

    public void Init()
    {
        model = new InputModel();
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Drag.started += OnPressStart;
        playerInput.Player.Drag.canceled += OnPressEnd;
    }

    private void OnPressStart(CallbackContext context)
    {
        model.ChangeStartPosition(playerInput.Player.PointerPosition.ReadValue<Vector2>());
        model.IsOnTouch?.Invoke(true);
        Debug.Log(model.StartPosition.ToString());
    }

    private void OnPressEnd(CallbackContext context)
    {
        model.IsOnTouch?.Invoke(false);
    }

    public void Dispose()
    {
        playerInput.Player.Drag.started -= OnPressStart;
        playerInput.Player.Drag.canceled -= OnPressEnd;
        playerInput.Disable();
    }

    public void LocalUpdate()
    {
        model.ChangePointerPosition(playerInput.Player.PointerPosition.ReadValue<Vector2>());
    }
}
