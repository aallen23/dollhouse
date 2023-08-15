using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] songs;    //holds all ambience "songs"

    private bool playAmbience,      //bool set based on whether ambience should be playing
        paused;                     //bool set for game pause

    [Tooltip("Assign all Audio sounds here")]
    [SerializeField]
    private AudioSource menu,       //contains menu music
        ambience,                   //contains all ambience tracks as children
        UI_hover,                   //contains button hover sound effect
        UI_click,                   //contains button clicked sound effect
        puzzle,                     //contains puzzle solved sound effect
        memory,                     //contains base memory music
        piano,                      //contains music for solving the piano puzzle
        pageFlip,                   //contains page flip sound effect
        carScene,                   //contains car scene audio
        memoryFinale,               //contains memory music finale version for end of game
        drawerOpen,                 //contains draweropen sound effect
        drawerClosed,               //contains drawerclosed sound effect
        endCredits,                 //contains endcredits music
        currentSound,               //contains current ambience
        currentMusic;               //contains current music such as menu or credits music

    public void Awake()
    {
        //collects all ambience sounds into a list
        songs = ambience.GetComponentsInChildren<AudioSource>();
        //sets bools to false and currentsound to null
        playAmbience = false;
        paused = false;
        currentSound = null;
        //plays menu music
		MenuMusic();
    }

    public void Update()
    {
        //if ambient sound should be playing, and game is not paused, get new ambience sound to play
        if (currentSound != null && !currentSound.isPlaying && playAmbience == true && paused == false)
        {
            GetNewAmbience();
        }
    }

    //turns off main menu music
    public void TurnOffMusic()
    {
        menu.Stop();
    }


    //gets new random ambience from songs list
    public void GetNewAmbience()
    {
        currentSound = songs[Random.Range(0, songs.Length - 1)];
        currentSound.Play();
    }


    //triggers ambience to start playing
    //triggered through yarnspinner script
    [YarnCommand("StartAmbience")]
    public void StartAmbience()
    {
        playAmbience = true;
        GetNewAmbience();
    }

	[YarnCommand("StartIntro")]
	public void Intro()
	{
		StartCoroutine(IntroCo());
	}

	IEnumerator IntroCo()
	{
		if (!GameSettings.debugMode)
		{
			PlayCarScene();
			yield return new WaitForSeconds(14f);
		}
		yield return new WaitForSeconds(1f);
		FindObjectOfType<DialogueRunner>().StartDialogue("StartGame2");
	}

    //pauses ambience music but saves current ambience
    public void PauseAmbience()
    {
        paused = true;
        if(currentSound != null)
            currentSound.Pause();
    }

    //unpauses current ambience
    public void UnpauseAmbience()
    {
        paused = false;
        currentSound.Play();
    }

    //plays menu music
    public void MenuMusic()
    {
        currentMusic = menu;
        menu.Play();
    }

    //plays memory music when memories are triggered, and then returns to ambience
    public void PlayMemorySound()
    {
        PauseAmbience();
        currentMusic.Stop();
        currentMusic = memory;
        memory.Play();
        float clipLength = memory.clip.length;
        StartCoroutine(WaitForMusic(clipLength));       //uses WaitForMusic coroutine to wait for Memory music to finish before retriggering ambience
    }


    //plays final memory music for game finale
    //triggered through yarnspinner script
    [YarnCommand("PlayFinalMusic")]
    public void PlayMemoryHerb()
    {
        PauseAmbience();
        currentMusic.Stop();
        currentMusic = memoryFinale;
        memoryFinale.Play();
        float clipLength = memoryFinale.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }

    //play puzzle solved sound
    public void PlayPuzzleSound()
    {
        PauseAmbience();
        currentMusic.Stop();
        currentMusic = puzzle;
        puzzle.Play();
        float clipLength = puzzle.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }

    //plays music for solved piano puzzle
    public void PlayPiano()
    {
        PauseAmbience();
        currentMusic.Stop();
        currentMusic = piano;
        piano.Play();
        float clipLength = piano.clip.length;
        StartCoroutine(WaitForMusic(clipLength));
    }


    //plays credits music while credits are running
    public void PlayCreditsMusic()
    {
		if (currentMusic)
		{
			currentMusic.Stop();
		}
		if (paused == false)
		{
			PauseAmbience();
		}
		currentMusic = endCredits;
        endCredits.Play();
    }

    //stops credit music when leaving credits
    public void StopCreditsMusic()
    {
        endCredits.Stop();
    }

    //plays car intro during intro sequence
    //triggered through yarnspinner script
    [YarnCommand("PlayCarIntro")]
    public void PlayCarScene()
    {
        carScene.Play();
    }

    //plays on button hover sound
    public void OnButtonHover()
    {
        UI_hover.Play();
    }

    //plays on button click sound
    public void OnButtonClick()
    {
        UI_click.Play();
    }

    //plays page flip for pause screen sound
    public void PageFlip()
    {
        pageFlip.Play();
    }

    //plays drawer open sound
    public void PlayDrawerOpen()
    {
        drawerOpen.Play();
    }

    //plays drawer close sound
    public void PlayDrawerClosed()
    {
        drawerClosed.Play();
    }

    //coroutine that takes the length of music clip and waits for music to finish before unpausing ambience
    IEnumerator WaitForMusic(float time)
    {
        yield return new WaitForSeconds(time);
        UnpauseAmbience();
    }

}


