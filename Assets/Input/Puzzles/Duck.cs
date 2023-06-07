using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
	[Tooltip("The note, to show when He shows up.")] public GameObject note;
	[Tooltip("A list of honks that we can cycle through.")] public List<AudioClip> honks;
	[Tooltip("AudioSource for honks.")] public AudioSource source;

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
