using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] private Controls playerInput;
    private Mouse virtualMouse;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private float cursorSpeed = 2000;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;

    private bool previousMouseState;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        //InputUser.PerformPairingWithDevice(virtualMouse, playerInput.);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }


        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        //Delta 
        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        stickValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPositon = virtualMouse.position.ReadValue();

        Vector2 newPosition = currentPositon + stickValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 10, Screen.width - 10);
        newPosition.y = Mathf.Clamp(newPosition.y, 10, Screen.height - 10);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, stickValue);

        bool sButtonIsPressed = Gamepad.current.aButton.isPressed;
        if (previousMouseState != sButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);

            mouseState.WithButton(MouseButton.Left, sButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = sButtonIsPressed;
        }

        AnchorCursor(newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
