using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    private Animator animator;
    private bool blinking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        blinking = true;
        StartCoroutine("Blink");
    }


    IEnumerator Blink()
    {
        while (blinking)
        {
            animator.SetTrigger("isBlink");
            yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));
        }
    }

}
