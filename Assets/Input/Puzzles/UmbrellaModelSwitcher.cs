using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaModelSwitcher : MonoBehaviour
{
    public List<GameObject> umbrellaModels;
    public UmbrellaPuzzle puzzle;

	private void Start()
	{
		puzzle = FindObjectOfType<UmbrellaPuzzle>();
	}

	public void SwitchModel(int item)
    {
        foreach (GameObject umb in umbrellaModels)
        {
            if (umbrellaModels.IndexOf(umb) == item)
            {
                umb.SetActive(true);
                puzzle.UpdateUmbrellas(gameObject.name, umb.GetComponent<ObjectData>().addedItem);
            }
            else
            {
                umb.SetActive(false);
            }
        }
    }
}
