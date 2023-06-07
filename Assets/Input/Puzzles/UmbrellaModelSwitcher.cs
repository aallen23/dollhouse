using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaModelSwitcher : MonoBehaviour
{
    public List<GameObject> umbrellaModels;
    public UmbrellaPuzzle puzzle;
	public AudioSource putdownAudio;

	private void Start()
	{
		puzzle = FindObjectOfType<UmbrellaPuzzle>(true);
	}

	public void SwitchModel(int item)
    {
        foreach (GameObject umb in umbrellaModels)
        {
            if (umbrellaModels.IndexOf(umb) == item)
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
