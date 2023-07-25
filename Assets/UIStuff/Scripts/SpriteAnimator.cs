using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using GD.MinMaxSlider;

[RequireComponent(typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    //controls current facial emotion
    public enum Emotes { Neutral, Sad, Disgusted, Scared, Terrified, Angry };      //contains all possible facial emotions
    private Emotes currentEmote;            //saves current emotion

    [SerializeField, Tooltip("If true, the sprite can blink.")] private bool animateBlink = true;
    [SerializeField, MinMaxSlider(0, 30), Tooltip("The random range (in seconds) between blinking.")] private Vector2 blinkingFrequency;

    private Animator spriteAnimator;
    private bool isBlinking;
    private float currentBlinkWaitTime, blinkWaitTime;

    private void Awake()
    {
        //gets animator component from CeceFace
        spriteAnimator = GetComponent<Animator>();
        RandomizeBlinkTime();

        //sets animator to neutral
        Neutral();
    }

    private void OnEnable()
    {
        if (animateBlink)
        {
            isBlinking = true;
            RandomizeBlinkTime();
        }
    }

    private void OnDisable()
    {
        ResetAnimator();
        if (animateBlink)
            isBlinking = false;
    }

    void Update()
    {
        //If the character can blink, increment the blink timer
        if (animateBlink && isBlinking)
        {
            if (currentBlinkWaitTime > blinkWaitTime)
                Blink();
            else
                currentBlinkWaitTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Resets the current blink wait time and sets a new random blink wait time.
    /// </summary>
    private void RandomizeBlinkTime()
    {
        currentBlinkWaitTime = 0f;
        Random.InitState(System.DateTime.Now.Millisecond);
        blinkWaitTime = Random.Range(blinkingFrequency.x, blinkingFrequency.y);
    }

    /// <summary>
    /// Sets the blink trigger and re-randomizes the blink time.
    /// </summary>
    private void Blink()
    {
        spriteAnimator.SetTrigger("isBlink");
        RandomizeBlinkTime();
    }

    //sets animator to disgusted
    [YarnCommand("Disgusted")]
    public void Disgusted()
    {
        spriteAnimator.SetBool("isDisgusted", true);
        currentEmote = Emotes.Disgusted;
    }

    //sets animator to sad
    [YarnCommand("Sad")]
    public void Sad()
    {
        spriteAnimator.SetBool("isSad", true);
        currentEmote = Emotes.Sad;
    }

    //sets animator to angry
    [YarnCommand("Angry")]
    public void Angry()
    {
        spriteAnimator.SetBool("isAngry", true);
        currentEmote = Emotes.Angry;
    }

    //sets animator to scared
    [YarnCommand("Scared")]
    public void Scared()
    {
        spriteAnimator.SetBool("isScared", true);
        currentEmote = Emotes.Scared;
    }

    //sets animator to terrified
    [YarnCommand("Terrified")]
    public void Terrified()
    {
        spriteAnimator.SetBool("isTerrified", true);
        currentEmote = Emotes.Terrified;
    }

    //sets animator to neutral
    [YarnCommand("Neutral")]
    public void Neutral()
    {
        ResetAnimator();
        spriteAnimator.Play("Neutral");
        currentEmote = Emotes.Neutral;
        //blinking = true;
    }

    /// <summary>
    /// Resets all variables in the sprite animator.
    /// </summary>
    public void ResetAnimator()
    {
        spriteAnimator.SetBool("isDisgusted", false);
        spriteAnimator.SetBool("isSad", false);
        spriteAnimator.SetBool("isAngry", false);
        spriteAnimator.SetBool("isScared", false);
        spriteAnimator.SetBool("isTerrified", false);
    }

    /// <summary>
    /// Saves and plays the current emote animation.
    /// </summary>
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

    public Emotes GetCharacterEmote() => currentEmote;
}
