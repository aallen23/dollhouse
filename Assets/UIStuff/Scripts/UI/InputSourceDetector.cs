using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputSourceDetector : MonoBehaviour
{
    public static InputSourceDetector Instance;
    public enum Controls { NONE, KEYBOARD, GAMEPAD };
    private PlayerInput playerInput;

    internal Controls currentInputSource;

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
        playerInput.onControlsChanged += OnControlsChanged;
    }

    /// <summary>
    /// Function called with the player's current control scheme changes.
    /// </summary>
    /// <param name="player">The PlayerInput component data.</param>
    private void OnControlsChanged(PlayerInput player)
    {
        string lastInput = player.currentControlScheme;

        Controls newInput = Controls.NONE;

        //Get the input based on the current control scheme's last control scheme input name
        switch (lastInput)
        {
            case "Keyboard and Mouse":
                newInput = Controls.KEYBOARD;
                break;
            case "Gamepad":
                newInput = Controls.GAMEPAD;
                break;
        }

        //If the new input is different than what is currently stored, the control scheme has been successfully changed
        if(newInput != currentInputSource)
        {
            Debug.Log("Controls switched to " + newInput.ToString());
            currentInputSource = newInput;
            InvokeControlsChanged();
        }
    }

    /// <summary>
    /// Calls any functions on other objects that rely on the controls changing.
    /// </summary>
    private void InvokeControlsChanged()
    {
        //Update the game's cursor based on the control scheme's input
        GamepadCursor gamepadCursor = FindObjectOfType<GamepadCursor>();
        if(gamepadCursor != null)
            gamepadCursor.SwitchCursorInput();

        MenuManager menuSystem = FindObjectOfType<MenuManager>();
        if (menuSystem != null)
            menuSystem.SetDefaultSelectedButton();

        switch (currentInputSource)
        {
            case Controls.GAMEPAD:
                //If there are any dialogue options open when switching to gamepad, select the first one
                if (DialogueController.Instance != null)
                    DialogueController.Instance.SelectFirstOption();
                break;
        }
    }
}
