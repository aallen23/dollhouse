using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSprite : MonoBehaviour
{
    //activates npc sprite for dialogue system
    //triggered through yarnspinner script

    //activates npc sprite in ui
    [YarnCommand("activate")]
    public void ActivateSprite()
    {
        GetComponent<Image>().enabled = true;
    }

    //deactivates sprite
    [YarnCommand("deactivate")]
    public void DeActivateSprite()
    {
        GetComponent<Image>().enabled = false;
    }
}
