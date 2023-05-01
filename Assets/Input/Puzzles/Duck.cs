using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
	public GameObject note;

	private void Start()
	{
		note.SetActive(false);
	}

	public void ShowNote()
	{
		note.SetActive(true);
	}
}
