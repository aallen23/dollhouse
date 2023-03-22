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

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dollLantern,
        mainMenu1,
        mainMenu2,
        gameUI,
        credits,
        pause,
        quitFrame,
        optionsFrame,
        videoFrame,
        audioFrame,
        controlsFrame,
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
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool fadeToBlack)
    {
        Color c = blackscreen.GetComponent<Image>().color;

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

    public void SetAllInactive()
    {
        mainMenu1.SetActive(false);
        mainMenu2.SetActive(false);
        gameUI.SetActive(false);
        credits.SetActive(false);
        pause.SetActive(false);
        quitFrame.SetActive(false);
        optionsFrame.SetActive(false);
    }

    [YarnCommand("ActivateUI")]
    public void ActivateGameUI()
    {
        SetAllInactive();
        gameUI.SetActive(true);
    }

    public void StartButton()
    {
        SetAllInactive();
        audioManager.Ambience();
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
        SetAllInactive();
        optionsFrame.SetActive(true);
        audioFrame.SetActive(false);
        controlsFrame.SetActive(false);
        videoFrame.SetActive(true);
    }

    public void VideoButton()
    {
        audioFrame.SetActive(false);
        controlsFrame.SetActive(false);
        videoFrame.SetActive(true);
    }

    public void BrightnessSlide(float brightLvl)
    {
        color.postExposure.value = brightLvl;
    }

    public void ChangeDisplay()
    {
        TMP_Dropdown dropdown = videoFrame.transform.GetComponentInChildren<TMP_Dropdown>(true);
        if (dropdown.value == 1)
        {
            r = Screen.currentResolution;
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
            Screen.SetResolution(r.width, r.height, true);
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

    public void AudioButton()
    {
        videoFrame.SetActive(false);
        controlsFrame.SetActive(false);
        audioFrame.SetActive(true);
    }

    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", Mathf.Log10(masterLvl) * 20);
    }

    public void SetSFXLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log10(sfxLvl) * 20);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log10(musicLvl) * 20);
    }

    public void ControlsButton()
    {
        videoFrame.SetActive(false);
        audioFrame.SetActive(false);
        controlsFrame.SetActive(true);
    }

    public void Pause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        pause.SetActive(!pause.activeSelf);
    }

    public void ReturnToMain()
    {
        //audioManager.TurnOffMusic();
        //audioManager.MenuMusic();
        SetAllInactive();
        mainMenu1.SetActive(true);
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
