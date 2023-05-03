using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] songs;

    private bool playAmbience,
        paused;

    [SerializeField]
    private AudioSource menu,
        ambience,
        UI_hover,
        UI_click,
        puzzle,
        memory,
        pageFlip,
        carScene,
        memoryFinale,
        drawerOpen,
        drawerClosed,
        endCredits,
        currentSound;

    public void Awake()
    {
        songs = ambience.GetComponentsInChildren<AudioSource>();
        playAmbience = false;
        paused = false;
        currentSound = null;
    }

    public void Update()
    {
        if (currentSound != null && !currentSound.isPlaying && playAmbience == true && paused == false)
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

    [YarnCommand("StartAmbience")]
    public void StartAmbience()
    {
        playAmbience = true;
        GetNewAmbience();
    }

    public void PauseAmbience()
    {
        paused = true;
        currentSound.Pause();
    }

    public void UnpauseAmbience()
    {
        paused = false;
        currentSound.Play();
    }

    public void MenuMusic()
    {
        menu.Play();
    }

    public void PlayMemorySound()
    {
        PauseAmbience();
        memory.Play();
        float clipLength = memory.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }

    [YarnCommand("PlayFinalMusic")]
    public void PlayMemoryHerb()
    {
        PauseAmbience();
        memoryFinale.Play();
        float clipLength = memoryFinale.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }

    public void PlayPuzzleSound()
    {
        PauseAmbience();
        puzzle.Play();
        float clipLength = puzzle.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }

    public void PlayCreditsMusic()
    {
        menu.Stop();
        endCredits.Play();
    }

    public void StopCreditsMusic()
    {
        endCredits.Stop();
    }

    [YarnCommand("PlayCarIntro")]
    public void PlayCarScene()
    {
        carScene.Play();
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

    public void PlayDrawerOpen()
    {
        drawerOpen.Play();
    }

    public void PlayDrawerClosed()
    {
        drawerClosed.Play();
    }

    IEnumerator WaitForMusic(float time)
    {
        yield return new WaitForSeconds(time);
        UnpauseAmbience();
    }

}


