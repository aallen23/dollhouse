using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //references: https://gist.github.com/VeggieVampire/ea4cc2d07534f947bdad9284809856fc


    private float maxIntensity = 80.0f,
        minIntensity = 20.0f,
        minFlickerTime = 0.1f,
        maxFlickerTime = 0.04f;
    [SerializeField] private Light thislight;

    [SerializeField] private bool flicker;


    // Start is called before the first frame update
    void Start()
    {
        thislight = GetComponent<Light>();
        flicker = true;
        StartCoroutine(Flickering());
    }

    IEnumerator Flickering()
    {
        while (flicker)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            thislight.intensity = Random.Range(minIntensity, maxIntensity);
        }

    }

}
