using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
	public Animator crank, body, lid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void PlayHerb()
	{
		crank.SetTrigger("Crank");
		body.SetTrigger("Herb");
		lid.SetTrigger("FlipLid");
	}
}
