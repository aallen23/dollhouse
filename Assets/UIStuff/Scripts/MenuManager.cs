using System.Collections;
using System.Collections.Generic;
using TMPro;
using Yarn.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;

[System.Serializable]
public class PageArrows
{
    public Selectable[] arrowButtons;
}

public class MenuManager : MonoBehaviour
{
    public enum Menus { MAIN, PAGE1, PAGE2, PAGE3, PAGE4, PAUSE, CREDITS, QUIT }
    [SerializeField]
    private GameObject dollLantern,         //contains dollLantern to turn light flicker off
        ceceFace,                           //contains ceceface sprite from in game ui
        mainMenu1,                          //original main menu - no saves
        //mainMenu2,                        //second main menu - for save system
        gameUI,                             //in game ui
        credits,                            //credits scroll
        quitFrame,                          //quit frame triggered with quit button
        journal,                            //journal for pause and options menu
        page1,                              //page 1 of journal
        page2,                              //page 2 of journal
        page3,                              //page 3 of journal
        page4,                              //page 4 of journal
        mainPanel,                          //panel for page1 of journal - used if game is on main menu
        pausePanel,                         //panel for page1 of journal - used if game is on pause
        lighting,                           //lighting game object that contains all lights in game
        filterVol,                          //post processing global volume
        blackscreen,                        //blackscreen canvas object
        audioBox;                           //audio - holds all universal audio sources

    [SerializeField, Tooltip("The menu buttons to select by default when switching to gamepad. Ordered in terms of the Menus enum.")] private Selectable[] defaultMenuButtons;
    [SerializeField, Tooltip("The arrows on each journal page.")] private PageArrows[] journalArrowButtons;

    [SerializeField]
    private AudioMixer masterMixer;         //audio mixer
    private AudioManager audioManager;      //audio manager

    private bool flicker;                   //flicker bool to turn flicker on and off
    private Light[] lits;                   //list to contain all lights in the game
    private Volume postProVolume;           //global volume for post processing effects

    private ColorAdjustments color;         //global volume value for color adjustment
    private Bloom bloom;                    //global volume value for bloom
    private Vignette vg;                    //global volume value for vignette

    private bool bloomBool,                 //bloom bool
        vgBool;                             //vignette bool

    //private Resolution r;

    private DialogueRunner dialog;          //dialogue runner for ui
    public bool isPaused;                   //bool for pausing the game
    private CreditsScroll scrollScript;     //credits scroll script that controls credits moving

	//private int offsetx, offsety;

	public TextMeshProUGUI versionNumber;
	public Slider sliderMain, sliderMusic, sliderSFX;       //three options menu sliders for each audio mixer volume

	public TMP_Text fps;        //text to display fps

	private bool game_ended;                        //bool to store if game is ended
	private static bool restart_game = false;       //bool to restart game
	public AudioSource CeceCrumple;                 //audio source for cece crumple ( paper ripping )

	public Button page1_right, page4_left;
    private Menus previousMenu, currentMenu;
    private bool goBackToMenuOnMemories;
    private Controls playerControls;

    //finds lights, audio manager, dialogue, post processing volume values, and other important game objects on awake
    public void Awake()
    {
        playerControls = new Controls();
        playerControls.Menu.Pause.started += _ => TogglePause();
        playerControls.Menu.Cancel.started += _ => OnCancel();

        audioManager = audioBox.GetComponent<AudioManager>();
        dialog = FindObjectOfType<DialogueRunner>();
        lits = lighting.GetComponentsInChildren<Light>();
        postProVolume = filterVol.GetComponent<Volume>();
        postProVolume.profile.TryGet<Vignette>(out vg);
        postProVolume.profile.TryGet<Bloom>(out bloom);
        postProVolume.profile.TryGet<ColorAdjustments>(out color);
        flicker = true;
        bloomBool = true;
        vgBool = true;
        //SetAllInactive();
        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);

		masterMixer.GetFloat("masterVol", out float volMaster);
		sliderMain.value = undoLog(volMaster);
		masterMixer.GetFloat("musicVol", out float volMusic);
		sliderMusic.value = undoLog(volMusic);
		masterMixer.GetFloat("sfxVol", out float volSFX);
		sliderSFX.value = undoLog(volSFX);
        fps.gameObject.SetActive(GameSettings.showFPS);
    }

	private void Start()
	{
        GameManager.Instance.SetInMenu(true);

        CeceCrumple.gameObject.SetActive(false);
		if (restart_game)
		{
			restart_game = false;
			StartButton();
		}
	}

    private void OnEnable()
    {
        playerControls.Enable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetDefaultSelectedButton();
		versionNumber.text = Application.version;
    }

    private float undoLog(float num)
	{
		float newNum = num / 20;
		newNum = Mathf.Pow(10, num);
		return newNum;
	}

    private void Update()
    {

		//Debug.Log(Screen.width + " " + Screen.height);
		/*if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            //Debug.Log("yah");
            offsetx = 10;
            Screen.SetResolution(Screen.width + offsetx, Screen.height, true);
        }
        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            offsetx = 10;
            Screen.SetResolution(Screen.width - offsetx, Screen.height, true);
        }
        if (Keyboard.current.rightBracketKey.wasPressedThisFrame)
        {
            offsety = 10;
            Screen.SetResolution(Screen.width, Screen.height + offsety, true);
        }
        if (Keyboard.current.leftBracketKey.wasPressedThisFrame)
        {
            offsety = 10;
            Screen.SetResolution(Screen.width, Screen.height - offsety, true);
        }*/

	}
    
    //triggers fade coroutine true - black fades in
    [YarnCommand("fadeIn")]
    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    //triggers fade coroutine false - black fades out
    [YarnCommand("fadeOut")]
    public void FadeOut()
    {
		//FindObjectOfType<P2PCameraController>().desiredRotation.y = 180f; //disable if firstpos is in dollhouse
        StartCoroutine(Fade(false));
    }

	[YarnCommand("rip")]
	public void Rip()
	{
		FindObjectOfType<P2PCameraController>().Travel(FindObjectOfType<P2PCameraController>().lastPos);
		SetAllInactive();
		CeceCrumple.gameObject.SetActive(true);
		CeceCrumple.Play();
	}

    //fades in ending sequence
	[YarnCommand("fadeOutEnd")]
	public void FadeInEnd()
	{
		//FindObjectOfType<P2PCameraController>().desiredRotation.y = 180f; //disable if firstpos is in dollhouse
		StartCoroutine(Fade(false));
		game_ended = true;
	}

    //fades blackscreen in or out based on paramater bool
    //parameter: Bool fadetoblack, if true fades to black, if fals fades black out
	IEnumerator Fade(bool fadeToBlack)
    {
        //fade black in
        if (fadeToBlack)
        {
			blackscreen.GetComponent<Image>().raycastTarget = true;

			for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                blackscreen.GetComponent<Image>().color = new Color(0,0,0, i);
                yield return null;
            }
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
			FindObjectOfType<P2PCameraController>().Travel(FindObjectOfType<P2PCameraController>().firstPos);
		}
        //fade black out
        else
        {
			

			for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                blackscreen.GetComponent<Image>().color = new Color(0,0,0, i);
                yield return null;
            }
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
			blackscreen.GetComponent<Image>().raycastTarget = false;
		}
    }

    //starts coroutine for blink sequence
    [YarnCommand("BlinkSequence")]
    public void StartBlink()
    {
        StartCoroutine(Blink());
    }

    //fades blackscreen several times, simulating an eye blink
    //for game intro
    IEnumerator Blink()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime * 25)
        {
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.05f);

        for (float i = 1; i >= 0; i -= Time.deltaTime * 10)
        {
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(Random.Range(0.0f, 0.25f));
        for (float i = 0; i <= 1; i += Time.deltaTime * 15)
        {
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.1f);
        for (float i = 1; i >= 0; i -= Time.deltaTime * 5)
        {
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    //sets all menus inactive
    public void SetAllInactive()
    {
		mainMenu1.SetActive(false);
		//mainMenu2.SetActive(false);
		journal.SetActive(false);
		gameUI.SetActive(false);
		credits.SetActive(false);
		quitFrame.SetActive(false);
        //optionsFrame.SetActive(false);
    }

    //activates ingame ui
    [YarnCommand("ActivateUI")]
    public void ActivateGameUI(bool showUI = true)
    {
        SetAllInactive();
        gameUI.SetActive(showUI);
    }

    //activates credits
	[YarnCommand("ActivateCredits")]
	public void Credits()
	{
		CreditsButton();
		audioManager.PlayCreditsMusic();
	}

    //saved for future game save system
    //designed to reset dialogue variables for new save
    //public void ResetDialogue()
    //{
    //    //update with each new added variable storage in the dialogue

    //    dialog.VariableStorage.SetValue("$itemUsed", 0);
    //    dialog.VariableStorage.SetValue("$BBfed", false);
    //    dialog.VariableStorage.SetValue("$Music", false);
    //    dialog.VariableStorage.SetValue("$Tintro", true);
    //    dialog.VariableStorage.SetValue("$Tea", false);
    //}

    //resets credit scroll to original position
    public void ResetCreditsScroll()
    {
        scrollScript = credits.GetComponent<CreditsScroll>();
        if (scrollScript != null)
        {
            scrollScript.ResetScroll();
        }
    }

    //start button triggers new game, starting with intro dialogue
    public void StartButton()
    {
		if (game_ended)
		{
			restart_game = true;
			ReloadScene();
		}

        GameManager.Instance.isGameActive = true;
        GameManager.Instance.SetInMenu(false);
        EventSystem.current.SetSelectedGameObject(null);

        SetAllInactive();
        audioManager.TurnOffMusic();
        //audioManager.StartAmbience();
        FadeIn();
        dialog.StartDialogue("StartGame");
		page1_right.onClick.RemoveListener(Page4Button);
		page1_right.onClick.AddListener(Page2Button);
		page4_left.onClick.RemoveListener(Page1Button);
		page4_left.onClick.AddListener(Page2Button);
	}

    //starts credit scroll and music
    public void CreditsButton()
    {
        OnSwitchMenu(Menus.CREDITS);
        SetAllInactive();
        //audioManager.TurnOffMusic();
        //audioManager.MenuMusic();
        credits.SetActive(true);
        credits.GetComponent<CreditsScroll>().StartScroll();
    }

    //opens options menu
    public void OptionsButton()
    {

        if (journal.activeSelf)
        {
            if (FindObjectOfType<P2PCameraController>().gameStarted)
            {
                //gameUI.SetActive(true);
                //mainPanel.SetActive(false);
                pausePanel.SetActive(true);
                Page4Button();
            }
            else
            {
                //mainPanel.SetActive(true);
                Page4Button();
            }
        }
        else
        {
            SetAllInactive();
            journal.SetActive(true);
            mainPanel.SetActive(true);
            Page4Button();
        }
    }

    //brightness slider for options menu - changes post processing value
    public void BrightnessSlide(float brightLvl)
    {
        color.postExposure.value = brightLvl;
    }

    //change display from fullscreen to windowed in dropdown in options menu
    public void ChangeDisplay()
    {
        TMP_Dropdown dropdown = page4.transform.GetComponentInChildren<TMP_Dropdown>(true);
        if (dropdown.value == 1)
        {
            //r = Screen.currentResolution;
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
            //Screen.SetResolution(r.width, r.height, true);
        }
    }

    //goes through lights in scene and turns on and off flicker script for all lights
    //based on checkbox in options menu
    public void FlickerTrigger()
    {
        if (flicker)
        {
            flicker = !flicker;
            dollLantern.GetComponentInChildren<LightFlicker>().TurnFlickerOff();
            for(int i=0; i < lits.Length; i++)
            {
                if(lits[i].TryGetComponent<LightFlicker>(out LightFlicker flickerScript))
                {
                    flickerScript.TurnFlickerOff();
                }
            }
        }

        else
        {
            flicker = !flicker;
            dollLantern.GetComponentInChildren<LightFlicker>().TurnFlickerOn();
            for (int i = 0; i < lits.Length; i++)
            {
                if (lits[i].TryGetComponent<LightFlicker>(out LightFlicker flickerScript))
                {
                    flickerScript.TurnFlickerOn();
                }
            }
        }
    }

    //changes bool for bloom based on menu checkbox
    public void BloomTrigger()
    {
        bloomBool = !bloomBool;
        SetBloom();
    }

    //bloom trigger for options menu - changes post processing value based on bool
    public void SetBloom()
    {
        if (bloomBool)
        {
            bloom.intensity.value = 1.0f;
        }
        else
        {
            bloom.intensity.value = 0.0f;
        }
    }

    //changes bool for vignette based on menu checkbox
    public void VignetteTrigger()
    {
        vgBool = !vgBool;
        SetVignette();
    }

    //vignette trigger for options menu - changes post processing value based on bool
    public void SetVignette()
    {
        if (vgBool)
        {
            vg.intensity.value = 0.25f;
        }
        else
        {
            vg.intensity.value = 0.0f;
        }
    }

    //audio master slider for options menu - changes audio mixer value
    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", Mathf.Log10(sliderMain.value) * 20);
    }

    //audio sfx slider for options menu - changes audio mixer value
    public void SetSFXLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log10(sliderSFX.value) * 20);
    }

    //audio music slider for options menu - changes audio mixer value
    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log10(sliderMusic.value) * 20);
    }

    //sets page 1 of journal active
    public void Page1Button()
    {
        SetPagesInactive();
        page1.SetActive(true);
        OnSwitchMenu(isPaused ? Menus.PAUSE : Menus.PAGE1);
    }

    //sets page 2 of journal active
    public void Page2Button()
    {
        SetPagesInactive();
        page2.SetActive(true);
        OnSwitchMenu(Menus.PAGE2);
    }

    //sets page 3 of journal active
    public void Page3Button()
    {
        SetPagesInactive();
        page3.SetActive(true);
        OnSwitchMenu(Menus.PAGE3);
    }

    //sets page 4 of journal active
    public void Page4Button()
    {
        SetPagesInactive();
        page4.SetActive(true);
        OnSwitchMenu(Menus.PAGE4);
    }

    //sets all journal pages inactive
    public void SetPagesInactive()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }

    private void TogglePause()
    {
        Debug.Log("Pause Button Pressed.");
        Pause();
    }

    //pauses and unpauses game
    public void Pause()
    {
        if (!isPaused && !journal.activeSelf && !QuestManager.IsQuestMenuOpen())
        {
            isPaused = true;
            Time.timeScale = 0f;
            journal.SetActive(true);
			mainPanel.SetActive(false);
			Page1Button();
            pausePanel.SetActive(true);
        }
        else
        {
            if(journal.activeInHierarchy)
                journal.SetActive(false);

            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1f;
                GameManager.Instance.SetInMenu(false);
                EventSystem.current.SetSelectedGameObject(null);
                OnResume();
            }

            QuestLogManager questLogManager = QuestManager.Instance.GetQuestLog();

            if (questLogManager != null && questLogManager.IsQuestLogOpen())
                questLogManager.ShowQuestLog(false);
        }

        //if (optionsFrame.activeSelf)
        //{
        //    pause.SetActive(true);
        //    optionsFrame.SetActive(false);
        //}
        //else
        //{
        //    if (!isPaused && !optionsFrame.activeSelf)
        //    {
        //        isPaused = true;
        //        Time.timeScale = 0f;
        //        pause.SetActive(true);
        //    }
        //    else
        //    {
        //        isPaused = false;
        //        Time.timeScale = 1f;
        //        journal.SetActive(true);
        //    }
        //}
    }

    private void OnResume()
    {
        if (GameManager.Instance.isCutsceneActive)
        {
            DialogueController.Instance.SelectFirstOption();
        }
        else
        {
            ActivateGameUI();
        }
    }

    public void MemoriesButton()
    {
        SetAllInactive();
        journal.SetActive(true);
        OpenMemories();
    }

	public void OpenMemories(bool goBackToMenu = true, int pageNum = 2)
    {
        goBackToMenuOnMemories = goBackToMenu;
		if (pageNum == 2)
		{
			Page2Button();
		}
		else if (pageNum == 3)
		{
			Page3Button();
		}
    }

    public void ExitMemories()
    {
        if (!goBackToMenuOnMemories)
        {
            Pause();
        }
        else
        {
            OptionsMenuBack();
        }

        goBackToMenuOnMemories = true;
    }

    private void OnCancel()
    {
        Debug.Log("Cancel Button Pressed.");
        if (InputSourceDetector.Instance != null)
            EventSystem.current.SetSelectedGameObject(null);

        if (GameManager.Instance.inMenu)
        {
            switch (currentMenu)
            {
                case Menus.MAIN:
                    QuitButton();
                    break;
                case Menus.PAGE1:
                    if (!GameManager.Instance.IsGameplayActive())
                        ReturnToMain();
                    break;
                case Menus.PAGE2:
                case Menus.PAGE3:
                    ExitMemories();
                    break;
                case Menus.PAGE4:
                    OptionsMenuBack();
                    break;
                case Menus.PAUSE:
                    if (InputSourceDetector.Instance.currentInputSource == InputSourceDetector.Controls.GAMEPAD)
                        Pause();
                    break;
                case Menus.CREDITS:
                case Menus.QUIT:
                    ReturnToMain();
                    break;
            }
        }
    }

    public void OptionsMenuBack()
    {
        if (!GameManager.Instance.isGameActive)
            ReturnToMain();
        else
            Page1Button();
    }

    //returns to main menu
    public void ReturnToMain()
    {
        if (credits.activeSelf)
        {
            ResetCreditsScroll();
            audioManager.StopCreditsMusic();
            audioManager.MenuMusic();
        }
		else if (FindObjectOfType<P2PCameraController>().gameStarted)
		{
			ReloadScene();
		}
        //audioManager.TurnOffMusic();
        //audioManager.MenuMusic();
        SetAllInactive();

        GameManager.Instance.isGameActive = false;
        OnSwitchMenu(Menus.MAIN);
        mainMenu1.SetActive(true);

        //if (FindObjectOfType<P2PCameraController>().gameStarted)
        //{
        //    optionsFrame.SetActive(false);
        //    pause.SetActive(true);
        //}
        //else
        //{
        //    if (credits.activeSelf)
        //    {
        //        ResetCreditsScroll();
        //    }
        //    //audioManager.TurnOffMusic();
        //    //audioManager.MenuMusic();
        //    ResetDialogue();
        //    SetAllInactive();
        //    mainMenu1.SetActive(true);
        //}
    }

    //reloads the entire game
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        GameManager.Instance.isGameActive = false;
        GameManager.Instance.SetInMenu(true);
    }

    //quit button triggers quit frame that confirms player wants to quit
    public void QuitButton()
    {
        SetAllInactive();
        quitFrame.SetActive(true);
        OnSwitchMenu(Menus.QUIT);
    }

    //actual quit button quits game
    public void ActualQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    //changes quality settings in options menu
    public void ChangeQuality()
	{
		TMP_Dropdown dropdown = GameObject.Find("QualityDropdown").GetComponent<TMP_Dropdown>();
		QualitySettings.SetQualityLevel(dropdown.value);
		Debug.Log(QualitySettings.GetQualityLevel());
	}

    private void OnSwitchMenu(Menus newMenu)
    {
        GameManager.Instance.SetInMenu(true);
        previousMenu = currentMenu;
        currentMenu = newMenu;
        if(InputSourceDetector.Instance.currentInputSource == InputSourceDetector.Controls.GAMEPAD)
            SetDefaultSelectedButton();
    }

    public void SetDefaultSelectedButton()
    {
        if(InputSourceDetector.Instance != null)
            EventSystem.current.SetSelectedGameObject(null);

        if (GameManager.Instance != null && GameManager.Instance.inMenu)
        {
            EventSystem.current.SetSelectedGameObject(defaultMenuButtons[(int)currentMenu].gameObject);
        }
    }

}
