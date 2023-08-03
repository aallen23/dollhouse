using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UmbrellaModelSwitcher : MonoBehaviour
{
	[Tooltip("List of all Umbrellas.")] public List<GameObject> umbrellaModels;
	[Tooltip("The Puzzle Controller.")] private UmbrellaPuzzle puzzle;
	[Tooltip("Audio to play when placing Umbrellas.")] private AudioSource putdownAudio;

	private void Start()
	{
		puzzle = FindObjectOfType<UmbrellaPuzzle>(true);
		putdownAudio = GetComponent<AudioSource>();
	}

	//Called by ObjectData.cs
	public void SwitchModel(int item)
    {
        foreach (GameObject umb in umbrellaModels)
        {
            if (umbrellaModels.IndexOf(umb) == item) //If the item we are using on the Deck Box is an umbrella, we show that umbrella and hide the rest
            {
                umb.SetActive(true);
                puzzle.UpdateUmbrellas(gameObject.name, umb.GetComponent<ObjectData>().addedItem);
				putdownAudio.Play();
            }
            else
            {
                umb.SetActive(false);
            }
        }
    }
}
