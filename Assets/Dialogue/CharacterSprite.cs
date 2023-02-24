using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [YarnCommand("activate")]
    public void ActivateSprite()
    {
        gameObject.SetActive(true);
    }

}
