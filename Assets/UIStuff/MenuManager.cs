using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu1,
        mainMenu2,
        gameUI,
        quitFrame,
        optionsFrame,
        videoFrame,
        audioFrame,
        controlsFrame,
        volume;

    [SerializeField]
    private Volume postProVolume;

    [SerializeField]
    private Slider brightnessSlider;

    private ColorAdjustments color;
    private Bloom bloom;
    private Vignette vg;

    private bool bloomBool,
        vgBool;

    public void Start()
    {
        brightnessSlider.onValueChanged.AddListener(delegate { BrightnessSlide(); });
        postProVolume = volume.GetComponent<Volume>();
        postProVolume.profile.TryGet<Vignette>(out vg);
        postProVolume.profile.TryGet<Bloom>(out bloom);
        postProVolume.profile.TryGet<ColorAdjustments>(out color);
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
        videoFrame.SetActive(true);
    }

    public void VideoButton()
    {
        audioFrame.SetActive(false);
        controlsFrame.SetActive(false);
        videoFrame.SetActive(true);
    }

    public void BrightnessSlide()
    {
        color.postExposure.value = brightnessSlider.value;
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
