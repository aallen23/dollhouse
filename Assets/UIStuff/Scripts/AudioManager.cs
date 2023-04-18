using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] songs;

    private bool playAmbience;

    [SerializeField]
    private AudioSource menu,
        ambience,
        UI_hover,
        UI_click,
        puzzle,
        memory,
        pageFlip,
        currentSound;

    public void Awake()
    {
        songs = ambience.GetComponentsInChildren<AudioSource>();
        playAmbience = false;
        currentSound = null;
    }

    public void Update()
    {
        if (currentSound != null && !currentSound.isPlaying && playAmbience == true)
        {
            GetNewAmbience();
        }
    }

    public void TurnOffMusic()
    {
        menu.Stop();
        //for (int i = 0; i < songs.Length; i++)
        //{
        //    songs[i].Stop();
        //}
    }

    public void GetNewAmbience()
    {
        currentSound = songs[Random.Range(0, songs.Length - 1)];
        currentSound.Play();
    }

    public void StartAmbience()
    {
        playAmbience = true;
        GetNewAmbience();
    }

    public void MenuMusic()
    {
        menu.Play();
    }

    public void PlayMemorySound()
    {
        memory.Play();
    }

    public void PlayPuzzleSound()
    {
        puzzle.Play();
    }

    public void OnButtonHover()
    {
        UI_hover.Play();
    }

    public void OnButtonClick()
    {
        UI_click.Play();
    }

    public void PageFlip()
    {
        pageFlip.Play();
    }

}


