using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        controlsFrame;

    [SerializeField]
    private Slider brightnessSlider;

    public void Start()
    {
        brightnessSlider.onValueChanged.AddListener(delegate { BrightnessSlide(); });
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
        Debug.Log(brightnessSlider.value);
        Screen.brightness = brightnessSlider.value;
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
