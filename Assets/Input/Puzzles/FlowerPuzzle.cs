using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPuzzle : MonoBehaviour
{
	[Tooltip("The flower GameObject to show.")] public GameObject Flower;
	private bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//Called by CheckCry.cs when Cry Trigger collides with the FlowerPot GameObject
	public void Grow()
	{
		if (!grown)
		{
			Flower.SetActive(true);
			grown = true;
			GetComponent<AudioSource>().Play(); //Puzzle Complete music

			FindObjectOfType<P2PCameraController>().dialog.VariableStorage.SetValue("$flowerGrown", true); //Update text for the flower pot interaction.
		}
	}

}
