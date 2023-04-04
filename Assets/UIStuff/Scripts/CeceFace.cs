using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CeceFace : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

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
        animator.Play("Blank");
    }
}
