using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSprite : MonoBehaviour
{
    [YarnCommand("activate")]
    public void ActivateSprite()
    {
        GetComponent<Image>().enabled = true;
    }

    [YarnCommand("deactivate")]
    public void DeActivateSprite()
    {
        GetComponent<Image>().enabled = false;
    }
}
