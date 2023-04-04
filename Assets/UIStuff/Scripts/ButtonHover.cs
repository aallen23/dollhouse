using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    private AudioManager audioManager;

    void Start()
    {
        if(GameObject.Find("/Audio") != null)
        {
            audioManager = GameObject.Find("/Audio").GetComponent<AudioManager>();
        }
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        audioManager.OnButtonHover();
    }
}
