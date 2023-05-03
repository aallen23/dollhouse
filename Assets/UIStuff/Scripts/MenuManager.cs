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
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dollLantern,
        ceceFace,
        mainMenu1,
        mainMenu2,
        gameUI,
        credits,
        quitFrame,
        journal,
        page1,
        page2,
        page3,
        page4,
        mainPanel,
        pausePanel,
        lighting,
        filterVol,
        blackscreen,
        audioBox;

    [SerializeField]
    private AudioMixer masterMixer;
    private AudioManager audioManager;

    private bool flicker;
    private Light[] lits;
    private Volume postProVolume;

    private ColorAdjustments color;
    private Bloom bloom;
    private Vignette vg;

    private bool bloomBool,
        vgBool;

    private Resolution r;

    private DialogueRunner dialog;
    public bool isPaused;
    private CreditsScroll scrollScript;

    private int offsetx, offsety;

	public Slider sliderMain, sliderMusic, sliderSFX;

    public void Awake()
    {
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

    [YarnCommand("fadeIn")]
    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    [YarnCommand("fadeOut")]
    public void FadeOut()
    {
        FindObjectOfType<P2PCameraController>().Travel(FindObjectOfType<P2PCameraController>().firstPos);
		//FindObjectOfType<P2PCameraController>().desiredRotation.y = 180f; //disable if firstpos is in dollhouse
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool fadeToBlack)
    {

        if (fadeToBlack)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                blackscreen.GetComponent<Image>().color = new Color(0,0,0, i);
                yield return null;
            }
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                blackscreen.GetComponent<Image>().color = new Color(0,0,0, i);
                yield return null;
            }
            blackscreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    [YarnCommand("BlinkSequence")]
    public void StartBlink()
    {
        StartCoroutine(Blink());
    }

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

    public void SetAllInactive()
    {
        mainMenu1.SetActive(false);
        mainMenu2.SetActive(false);
        journal.SetActive(false);
        gameUI.SetActive(false);
        credits.SetActive(false);
        quitFrame.SetActive(false);
        //optionsFrame.SetActive(false);
    }

    [YarnCommand("ActivateUI")]
    public void ActivateGameUI()
    {
        SetAllInactive();
        gameUI.SetActive(true);
    }

    [YarnCommand("ActivateCece")]
    public void ActivateCece()
    {
        ceceFace.SetActive(true);
        ceceFace.GetComponent<CeceFace>().PlayCurrentEmote();
    }

    [YarnCommand("DeactivateCece")]
    public void DeactivateCece()
    {
        ceceFace.GetComponent<CeceFace>().SetBlinkFalse();
        ceceFace.SetActive(false);
    }

    //public void ResetDialogue()
    //{
    //    //update with each new added variable storage in the dialogue

    //    dialog.VariableStorage.SetValue("$itemUsed", 0);
    //    dialog.VariableStorage.SetValue("$BBfed", false);
    //    dialog.VariableStorage.SetValue("$Music", false);
    //    dialog.VariableStorage.SetValue("$Tintro", true);
    //    dialog.VariableStorage.SetValue("$Tea", false);
    //}

    public void ResetCreditsScroll()
    {
        scrollScript = credits.GetComponent<CreditsScroll>();
        if (scrollScript != null)
        {
            scrollScript.ResetScroll();
        }
    }

    public void StartButton()
    {
        SetAllInactive();
        audioManager.TurnOffMusic();
        //audioManager.StartAmbience();
        FadeIn();
        dialog.StartDialogue("StartGame");
    }

    public void CreditsButton()
    {
        SetAllInactive();
        //audioManager.TurnOffMusic();
        //audioManager.MenuMusic();
        credits.SetActive(true);
        credits.GetComponent<CreditsScroll>().StartScroll();
    }

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
            Page1Button();

        }
    }

    public void BrightnessSlide(float brightLvl)
    {
        color.postExposure.value = brightLvl;
    }

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

    public void BloomTrigger()
    {
        bloomBool = !bloomBool;
        SetBloom();
    }

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

    public void VignetteTrigger()
    {
        vgBool = !vgBool;
        SetVignette();
    }

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

    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", Mathf.Log10(sliderMain.value) * 20);
    }

    public void SetSFXLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log10(sliderSFX.value) * 20);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log10(sliderMusic.value) * 20);
    }

    public void Page1Button()
    {
        SetPagesInactive();
        page1.SetActive(true);
    }

    public void Page2Button()
    {
        SetPagesInactive();
        page2.SetActive(true);
    }

    public void Page3Button()
    {
        SetPagesInactive();
        page3.SetActive(true);
    }

    public void Page4Button()
    {
        SetPagesInactive();
        page4.SetActive(true);
    }

    public void SetPagesInactive()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }

    public void Pause()
    {
        if (!isPaused && !journal.activeSelf)
        {
            isPaused = true;
            //Time.timeScale = 0f;
            journal.SetActive(true);
			mainPanel.SetActive(false);
			Page1Button();
            pausePanel.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            journal.SetActive(false);
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

    public void ReturnToMain()
    {
        if (credits.activeSelf)
        {
            ResetCreditsScroll();
            audioManager.StopCreditsMusic();
            audioManager.MenuMusic();
        }
        //audioManager.TurnOffMusic();
        //audioManager.MenuMusic();
        SetAllInactive();
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

    public void Restart()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitButton()
    {
        SetAllInactive();
        quitFrame.SetActive(true);
    }

    public void ActualQuitButton()
    {
        Application.Quit();
    }

}
