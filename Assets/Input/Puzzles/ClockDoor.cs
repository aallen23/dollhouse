using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    [Tooltip("The Big Hand. We'll compare it to a desired value.")] public ObjectData handBig;
	[Tooltip("The Small Hand. We'll compare it to a desired value.")] public ObjectData handSmall;

	[Tooltip("Popsicles, we'll disable it when we make a desired comparison.")] public GameObject cup;
	[Tooltip("The Stairs, we'll enable it when we make a desired comparison.")] public GameObject stairs;

	[Tooltip("Whether the time has been successfully set. If it already has, we won't do anything.")] private bool completed;

    private void Start()
    {
		stairs.SetActive(false); //Stairs should start hidden.
	}

    public void Check()
    {
        Debug.Log(Mathf.Abs(handBig.transform.localEulerAngles.z - 300f).ToString("0.0") + " " + Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f).ToString("0.0"));

        if (Mathf.Abs(handBig.transform.localEulerAngles.z - 300f) < 15f && Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f) < 15f && !completed) //If the hands are close to our desired values (which is the time 2:20)
        {
			completed = true;
			stairs.SetActive(true);
			cup.SetActive(false);
			FindObjectOfType<P2PCameraController>().dialog.VariableStorage.SetValue("$stairs", true);
			GetComponent<AudioSource>().Play();
		}
    }
}
