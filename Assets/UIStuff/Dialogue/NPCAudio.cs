using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCAudio : MonoBehaviour
{
    //plays audio for NPC when clicked on in dialogue

    //contains a list of all audiosources for npc character
    private AudioSource[] sounds;
    
    void Awake()
    {
        //gets all audiosources on the npc
        sounds = GetComponents<AudioSource>();
    }

    //plays randomized npc audio
    //triggered by yarn script
    [YarnCommand("PlaySound")]
    public void PlaySound()
    {
        sounds[Random.Range(0, sounds.Length - 1)].Play();
    }
}
