using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAudio : MonoBehaviour
{
	public AudioClip fixedAudio;
	public AudioSource sourcePlop;
	public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Fix()
	{
		sourcePlop.Play();
		source.clip = fixedAudio;
		source.Play();
	}
}
