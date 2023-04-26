using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCry : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "FlowerPot")
		{
			other.GetComponent<FlowerPuzzle>().Grow();
		}
	}
}
