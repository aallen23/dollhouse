using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] songs;

    [SerializeField]
    private AudioSource menu,
        ambience1;

    public void Awake()
    {
        songs = GetComponentsInChildren<AudioSource>();
    }

    public void TurnOffMusic()
    {
        for (int i = 0; i < songs.Length; i++)
        {
            songs[i].Stop();
        }
    }

    public void MenuMusic()
    {
        menu.Play();
    }

    public void Ambience()
    {
        ambience1.Play();
    }
}
