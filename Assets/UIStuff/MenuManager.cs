using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        quitFrame,
        optionsFrame,
        videoFrame,
        audioFrame,
        controlsFrame,
        lighting,
        filterVol;

    [SerializeField]
    private AudioMixer masterMixer;

    private bool flicker;
    private Light[] lits;
    private Volume postProVolume;

    private ColorAdjustments color;
    private Bloom bloom;
    private Vignette vg;

    private bool bloomBool,
        vgBool;

    private Resolution r;

    public void Start()
    {
        lits = lighting.GetComponentsInChildren<Light>();
        postProVolume = filterVol.GetComponent<Volume>();
        postProVolume.profile.TryGet<Vignette>(out vg);
        postProVolume.profile.TryGet<Bloom>(out bloom);
        postProVolume.profile.TryGet<ColorAdjustments>(out color);
        flicker = true;
        bloomBool = true;
        vgBool = true;
    }


    public void SetAllInactive()
    {
        mainMenu1.SetActive(false);
        mainMenu2.SetActive(false);
        gameUI.SetActive(false);
        quitFrame.SetActive(false);
        optionsFrame.SetActive(false);
    }

    public void StartButton()
    {
        mainMenu1.SetActive(false);
        quitFrame.SetActive(false);
        optionsFrame.SetActive(false);
        gameUI.SetActive(true);
    }

    public void CreditsButton()
    {

    }

    public void OptionsButton()
    {
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

    public void Resolution()
    {
        if (Screen.fullScreen)
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

    public void ReturnToMain()
    {
        gameUI.SetActive(false);
        quitFrame.SetActive(false);
        optionsFrame.SetActive(false);
        mainMenu1.SetActive(true);
    }

    public void QuitButton()
    {
        quitFrame.SetActive(true);
    }

    public void ActualQuitButton()
    {
        Application.Quit();
    }

}
