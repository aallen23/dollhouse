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
        GetComponent<RawImage>().enabled = true;
    }

    [YarnCommand("deactivate")]
    public void DeActivateSprite()
    {
        GetComponent<RawImage>().enabled = false;
    }
}
