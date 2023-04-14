using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CeceFace : MonoBehaviour
{
    [SerializeField]
    private Sprite NeutralSprite;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool blinking;

    enum Emotes { Neutral, Sad, Disgusted, Scared, Terrified, Angry };
    Emotes currentEmote;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentEmote = Emotes.Neutral;
        Neutral();
    }

    void Update()
    {
        if (currentEmote == Emotes.Neutral && !blinking)
        {
            blinking = true;
            StartCoroutine("Blink");
        }
        else if (currentEmote == Emotes.Disgusted)
        {
            blinking = false;
        }
    }

    public void SetBlinkFalse()
    {
        blinking = false;
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
        animator.SetBool("isDisgusted", true);
        currentEmote = Emotes.Disgusted;
    }

    [YarnCommand("Sad")]
    public void Sad()
    {
        animator.SetBool("isSad", true);
        currentEmote = Emotes.Sad;
    }

    [YarnCommand("Angry")]
    public void Angry()
    {
        animator.SetBool("isAngry", true);
        currentEmote = Emotes.Angry;
    }

    [YarnCommand("Scared")]
    public void Scared()
    {
        animator.SetBool("isScared", true);
        currentEmote = Emotes.Scared;
    }

    [YarnCommand("Terrified")]
    public void Terrified()
    {
        animator.SetBool("isTerrified", true);
        currentEmote = Emotes.Terrified;
    }

    [YarnCommand("Neutral")]
    public void Neutral()
    {
        SetAllAnimsFalse();
        GetComponent<Image>().sprite = NeutralSprite;
        Debug.Log("sprite set to neutral");
        animator.Play("Neutral");
        currentEmote = Emotes.Neutral;
        //blinking = true;
    }

    public void SetAllAnimsFalse()
    {
        animator.SetBool("isDisgusted", false);
        animator.SetBool("isSad", false);
        animator.SetBool("isAngry", false);
        animator.SetBool("isScared", false);
        animator.SetBool("isTerrified", false);
    }


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
