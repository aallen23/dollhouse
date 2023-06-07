using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to Doll, only active while crying
public class CheckCry : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		//When this Trigger collides with the FlowerPot object
		if (other.gameObject.name == "FlowerPot")
		{
			other.GetComponent<FlowerPuzzle>().Grow();
		}
	}
}
