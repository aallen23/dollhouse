using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    internal bool isGameActive;
    internal bool isCutsceneActive;
    internal bool inMenu;
    internal bool gamepadCursorActive;
    internal string currentGameLocation;
    internal bool inDollhouse = false;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Function called when the player switches between the interior / exterior of the Doll House.
    /// </summary>
    public void OnSwitchPerspective()
    {
        if (isGameActive)
        {
            LocationName locationNameLabel = FindObjectOfType<LocationName>();
            if (locationNameLabel != null)
            {
                if (inDollhouse)
                {
                    //Function for when you enter the Dollhouse
                    //Debug.Log("Player is in the Doll House.");
                    if (!locationNameLabel.IsLabelShowing())
                        locationNameLabel.ShowLabel(true);
                }
                else
                {
                    //Function for when you exit the Dollhouse
                    //Debug.Log("Player is outside of the Doll House.");
                    if (locationNameLabel.IsLabelShowing())
                        locationNameLabel.ShowLabel(false);
                }
            }
        }
    }

    public void SetGamepadCursorActive(bool cursorActive)
    {
        Debug.Log("Gamepad Cursor Active: " + cursorActive);
        gamepadCursorActive = cursorActive;
        UpdateGamepadCursor();
    }

    public void SetInMenu(bool isInMenu)
    {
        Debug.Log("In Menu: " + isInMenu);
        inMenu = isInMenu;
        UpdateGamepadCursor();
    }

    public void SetCutsceneActive(bool isInCutscene)
    {
        Debug.Log("Cutscene Active: " + isInCutscene);
        isCutsceneActive = isInCutscene;
        UpdateGamepadCursor();
    }

    private void UpdateGamepadCursor()
    {
        //Update the game's cursor based on the control scheme's input
        GamepadCursor gamepadCursor = FindObjectOfType<GamepadCursor>();
        if (gamepadCursor != null)
            gamepadCursor.EnableCursor(gamepadCursorActive && !inMenu && !isCutsceneActive);
    }
}
