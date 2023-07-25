using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        if (inDollhouse)
        {
            //Function for when you enter the Dollhouse
            //Debug.Log("Player is in the Doll House.");
            if (!FindObjectOfType<LocationName>().IsLabelShowing())
                FindObjectOfType<LocationName>().ShowLabel(true);
        }
        else
        {
            //Function for when you exit the Dollhouse
            //Debug.Log("Player is outside of the Doll House.");
            if (FindObjectOfType<LocationName>().IsLabelShowing())
                FindObjectOfType<LocationName>().ShowLabel(false);
        }
    }
}