using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCAudio : MonoBehaviour
{

    private AudioSource[] sounds;
    
    void Awake()
    {
        sounds = GetComponents<AudioSource>();
    }

    [YarnCommand("PlaySound")]
    public void PlaySound()
    {
        sounds[Random.Range(0, sounds.Length - 1)].Play();
    }
}
