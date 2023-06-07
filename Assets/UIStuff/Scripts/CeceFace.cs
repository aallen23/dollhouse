using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CeceFace : MonoBehaviour
{
    [Tooltip("The CeceFace animator")]
    [SerializeField]
    private Animator animator;
    [Tooltip("is CeceFace blinking")]
    [SerializeField]
    private bool blinking;

    //controls current facial emotion
    enum Emotes { Neutral, Sad, Disgusted, Scared, Terrified, Angry };      //contains all possible facial emotions
    Emotes currentEmote;            //saves current emotion

    // Start is called before the first frame update
    void Start()
    {
        //gets animator component from CeceFace
        animator = GetComponent<Animator>();

        //sets animator to neutral
        Neutral();
    }

    void Update()
    {
        //sets coroutine to blink if face is neutral
        if (currentEmote == Emotes.Neutral && !blinking)
        {
            blinking = true;
            StartCoroutine("Blink");
        }
        else
        {
            blinking = false;
        }
    }

    //turns off blinking
    public void SetBlinkFalse()
    {
        blinking = false;
    }


    //coroutine to set blink animation
    IEnumerator Blink()
    {
        while (blinking)
        {
            animator.SetTrigger("isBlink");
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            blinking = false;
        }
    }

    //sets animator to disgusted
    [YarnCommand("Disgusted")]
    public void Disgusted()
    {
        animator.SetBool("isDisgusted", true);
        currentEmote = Emotes.Disgusted;
    }

    //sets animator to sad
    [YarnCommand("Sad")]
    public void Sad()
    {
        animator.SetBool("isSad", true);
        currentEmote = Emotes.Sad;
    }

    //sets animator to angry
    [YarnCommand("Angry")]
    public void Angry()
    {
        animator.SetBool("isAngry", true);
        currentEmote = Emotes.Angry;
    }

    //sets animator to scared
    [YarnCommand("Scared")]
    public void Scared()
    {
        animator.SetBool("isScared", true);
        currentEmote = Emotes.Scared;
    }

    //sets animator to terrified
    [YarnCommand("Terrified")]
    public void Terrified()
    {
        animator.SetBool("isTerrified", true);
        currentEmote = Emotes.Terrified;
    }

    //sets animator to neutral
    [YarnCommand("Neutral")]
    public void Neutral()
    {
        SetAllAnimsFalse();
        animator.Play("Neutral");
        currentEmote = Emotes.Neutral;
        //blinking = true;
    }

    //sets all animations to false in animator
    public void SetAllAnimsFalse()
    {
        animator.SetBool("isDisgusted", false);
        animator.SetBool("isSad", false);
        animator.SetBool("isAngry", false);
        animator.SetBool("isScared", false);
        animator.SetBool("isTerrified", false);
    }

    //takes current emote and plays it - saves animation info across dialogue sections
    public void PlayCurrentEmote()
    {
        switch (currentEmote)
        {
            case Emotes.Neutral:
                {
                    Neutral();
                    break;
                }
            case Emotes.Sad:
                {
                    Sad();
                    break;
                }
            case Emotes.Angry:
                {
                    Angry();
                    break;
                }
            case Emotes.Disgusted:
                {
                    Disgusted();
                    break;
                }
            case Emotes.Scared:
                {
                    Scared();
                    break;
                }
            case Emotes.Terrified:
                {
                    Terrified();
                    break;
                }
        }

    }

}
