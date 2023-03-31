using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] songs;

    [SerializeField]
    private AudioSource menu,
        UI_hover,
        UI_click;

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
        //make this play through ambience sounds randomly
        //ambience1.Play();
    }

    public void OnButtonHover()
    {
        UI_hover.Play();
    }

    public void OnButtonClick()
    {
        UI_click.Play();
    }

}
