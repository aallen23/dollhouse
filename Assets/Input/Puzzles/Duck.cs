using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
	public GameObject note;
	public List<AudioClip> honks;
	public AudioSource source;

	private void Start()
	{
		note.SetActive(false);
	}

	public void ShowNote()
	{
		note.SetActive(true);
	}

	public void Quack()
	{
		source.PlayOneShot(honks[Random.Range(0, honks.Count)]);
	}
}
