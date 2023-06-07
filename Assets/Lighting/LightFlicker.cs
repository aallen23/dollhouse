using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //references: https://gist.github.com/VeggieVampire/ea4cc2d07534f947bdad9284809856fc

    //sets variables for randomized intensity change causing light flicker
    [Tooltip("min and max values for timer and intensity")]
    [SerializeField]
    public float maxIntensity = 80.0f,
        minIntensity = 20.0f,
        minFlickerTime = 0.1f,
        maxFlickerTime = 0.04f;

    //contains light object that flickers
    [Tooltip("light to flicker")]
    [SerializeField] private Light thislight;

    //contains bool for flicker
    [Tooltip("is light flicker on")]
    [SerializeField] public bool flicker;


    // Start is called before the first frame update
    void Start()
    {
        thislight = GetComponent<Light>();
        flicker = true;
        StartCoroutine(Flickering());
    }

    //coroutine to cause light flicker
    //changes light intensity on random timer
    IEnumerator Flickering()
    {
        while (flicker)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            thislight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }

    //turns flicker off for options menu
    public void TurnFlickerOff()
    {
        flicker = false;
    }

    //turns flicker on
    public void TurnFlickerOn()
    {
        flicker = true;
        StartCoroutine(Flickering());
    }

}
