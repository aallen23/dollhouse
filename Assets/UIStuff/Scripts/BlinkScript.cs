using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    //unused script
    //absorbed by CeceFace script which controls all other Cece facial animations


    private Animator animator;
    private bool blinking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("Blink");
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Neutral"))
        {
            blinking = true;
        }

        else
        {
            blinking = false;
        }
    }


    IEnumerator Blink()
    {
        while (blinking)
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));
            animator.SetTrigger("isBlink");
        }
    }

}
