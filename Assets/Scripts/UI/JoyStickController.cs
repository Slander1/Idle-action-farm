using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    private PlayerInput _touchControls;
    [SerializeField] CanvasGroup _joystickCanvasGroup;

    private void Awake()
    {
        _joystickCanvasGroup.alpha = 0;
        _touchControls = new PlayerInput();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        _touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void OnDisable()
    {
        _touchControls.Disable();
        _touchControls.Touch.TouchPress.started -= ctx => StartTouch(ctx);
        _touchControls.Touch.TouchPress.canceled -= ctx => EndTouch(ctx);
    }

    private void StartTouch(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _joystickCanvasGroup.transform.position = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        _joystickCanvasGroup.alpha = 1;
        Debug.Log(_touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }

    private void EndTouch(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _joystickCanvasGroup.alpha = 0;
    }

    


}
