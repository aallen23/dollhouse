using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

//Adapted from https://youtube.com/watch?v=Y3WNwl1ObC8&feature=shares (Some of it was outdated/irrelevent)
public class GamepadCursor : MonoBehaviour
{
    [Tooltip("The Input Map we are using.")] [SerializeField] private Controls playerInput;
    private Mouse virtualMouse;
    [Tooltip("The Transform of the Gamepad Cursor.")] [SerializeField] private RectTransform cursorTransform;
    [Tooltip("The Gamepad Cursor Speed.")] [SerializeField] private float cursorSpeed;
    [Tooltip("The Transform of the UI Canvas.")] [SerializeField] private RectTransform canvasTransform;
    [Tooltip("The UI Canvas.")] [SerializeField] private Canvas canvas;
    private Camera mainCamera;

    private bool previousMouseState;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        //If we don't have a virtualMouse, create it and add the device.
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added) //If it's been created but not added, add it
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
        //Making sure the virtualMouse gets removed, otherwise it will stick around, even after the game stops.
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
