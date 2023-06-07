using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAudio : MonoBehaviour
{
	[Tooltip("Ambient clock sounds to switch to and play when it is fixed.")] public AudioClip fixedAudio;
	[Tooltip("Clock hand placing sound effect Source to play.")] public AudioSource sourcePlop;
	[Tooltip("Ambient clock sound effect Source.")] public AudioSource source;

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
		source.clip = fixedAudio; //source initial Clip is the broken one, now we're switching to the fixed version.
		source.Play();
	}
}
