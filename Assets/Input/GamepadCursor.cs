using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

//Adapted from https://youtube.com/watch?v=Y3WNwl1ObC8&feature=shares (Some of it was outdated/irrelevent)
public class GamepadCursor : MonoBehaviour
{
    [Tooltip("The Input Map we are using.")] [SerializeField] private Controls playerInput;
    private Mouse virtualMouse;
    [Tooltip("The Transform of the Gamepad Cursor.")] [SerializeField] private RectTransform cursorTransform;
    [Tooltip("The Transform of the UI Canvas.")] [SerializeField] private RectTransform canvasTransform;
    [Tooltip("The UI Canvas.")] [SerializeField] private Canvas canvas;
    [Tooltip("Warp mouse on virtual mouse position on control changing to keyboard.")][SerializeField] private bool warpToVirtualMousePosition = true;
    [Space(10)]
    [Header("Cursor Settings")]
    [Tooltip("The Gamepad Cursor Speed.")][SerializeField] private float cursorSpeed;
    [Tooltip("The deadzone value for making sure the cursor doesn't move with small movements.")][SerializeField] private float cursorDeadzone;


    private Camera mainCamera;

    private bool previousMouseState;
    private bool cursorEnabled;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        cursorEnabled = true;

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
        if (!cursorEnabled)
        {
            return;
        }

        if (virtualMouse == null || Gamepad.current == null)
        {
            AnchorCursor(Mouse.current.position.ReadValue());
            return;
        }

        //Delta 
        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();

        if (stickValue.magnitude > cursorDeadzone)
        {
            stickValue *= cursorSpeed * Time.deltaTime;
        }
        else
        {
            stickValue = Vector2.zero;
            
            if (InputSourceDetector.Instance.currentInputSource == InputSourceDetector.Controls.KEYBOARD)
            {
                AnchorCursor(Mouse.current.position.ReadValue());

                bool asButtonIsPressed = Gamepad.current.aButton.isPressed;
                //Debug.Log(sButtonIsPressed);
                if (previousMouseState != asButtonIsPressed)
                {
                    virtualMouse.CopyState<MouseState>(out var mouseState);

                    mouseState.WithButton(MouseButton.Left, asButtonIsPressed);
                    InputState.Change(virtualMouse, mouseState);
                    previousMouseState = asButtonIsPressed;
                }

                return;
            }
        }

        Vector2 currentPositon = virtualMouse.position.ReadValue();

        Vector2 newPosition = currentPositon + stickValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 10, Screen.width - 10);
        newPosition.y = Mathf.Clamp(newPosition.y, 10, Screen.height - 10);
        //Debug.Log(newPosition);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, stickValue);

        bool sButtonIsPressed = Gamepad.current.aButton.isPressed;
        //Debug.Log(sButtonIsPressed);
        if (previousMouseState != sButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);

            mouseState.WithButton(MouseButton.Left, sButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = sButtonIsPressed;
        }

        AnchorCursor(newPosition);
    }

    /// <summary>
    /// Function called by the InputSourceDetector when the control scheme is changed.
    /// </summary>
    public void SwitchCursorInput()
    {
        if(InputSourceDetector.Instance != null)
        {
            switch (InputSourceDetector.Instance.currentInputSource)
            {
                case InputSourceDetector.Controls.KEYBOARD:
                    //Moves the system's mouse to the position of the virtual mouse
                    if (warpToVirtualMousePosition)
                        Mouse.current.WarpCursorPosition(virtualMouse.position.ReadValue());
                    EnableCursor(true);
                    break;

                case InputSourceDetector.Controls.GAMEPAD:
                    //Moves the virtual mouse to the position of the system's mouse
                    InputState.Change(virtualMouse.position, Mouse.current.position.ReadValue());
                    EnableCursor(GameManager.Instance.gamepadCursorActive && !GameManager.Instance.inMenu && !GameManager.Instance.isCutsceneActive);
                    break;
            }
        }
    }

    private void UpdateCursorVisibility()
    {
        cursorTransform.GetComponent<CanvasGroup>().alpha = cursorEnabled ? 1 : 0;
    }

    public void EnableCursor(bool enableCursor)
    {
        cursorEnabled = enableCursor;
        UpdateCursorVisibility();
    }

    public void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        //Debug.Log(anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
