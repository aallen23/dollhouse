using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    internal bool isGameActive;
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
}
