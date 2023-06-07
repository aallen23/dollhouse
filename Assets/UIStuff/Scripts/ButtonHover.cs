using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    //plays audio on mouse hover over button

    //audio manager for game
    private AudioManager audioManager;

    //find audio manager
    void Start()
    {
        if(GameObject.Find("Audio") != null)
        {
            audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
        }
    }

    //if mouse hovers over button, play button hover sound
    public void OnPointerEnter(PointerEventData ped)
    {
        audioManager.OnButtonHover();
    }
}
