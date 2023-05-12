using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Herb : MonoBehaviour
{
	public Animator crank, body, lid;
	private bool cranked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	[YarnCommand("OpenHerb")]
	public void PlayHerb()
	{
		if (!cranked)
		{
			cranked = true;
			crank.SetTrigger("Crank");
			body.SetTrigger("Herb");
			lid.SetTrigger("FlipLid");
		}
	}
}
