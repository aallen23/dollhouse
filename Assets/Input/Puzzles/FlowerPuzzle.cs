using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPuzzle : MonoBehaviour
{
	public GameObject Flower;
	private bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Grow()
	{
		if (!grown)
		{
			Flower.SetActive(true);
			grown = false;
			GetComponent<AudioSource>().Play();
		}
	}

}
