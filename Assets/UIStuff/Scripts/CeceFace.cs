using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CeceFace : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool blinking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && !blinking)
        {
            blinking = true;
            StartCoroutine("Blink");
        }
    }

    IEnumerator Blink()
    {

        while (blinking)
        {
            animator.SetTrigger("isBlink");

            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            blinking = false;
        }
    }

    [YarnCommand("Disgusted")]
    public void Disgusted()
    {
        animator.Play("Disgusted");
    }

    [YarnCommand("Sad")]
    public void Sad()
    {
        animator.Play("Sad");
    }

    [YarnCommand("Angry")]
    public void Angry()
    {
        animator.Play("Angry");
    }

    [YarnCommand("Scared")]
    public void Scared()
    {
        animator.Play("Scared");
    }

    [YarnCommand("Terrified")]
    public void Terrified()
    {
        animator.Play("Terrified");
    }

    [YarnCommand("Neutral")]
    public void NeutralFace()
    {
        animator.Play("Neutral");
    }

}
