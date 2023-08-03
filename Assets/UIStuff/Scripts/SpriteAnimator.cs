using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

[RequireComponent(typeof(Image), typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    //controls current facial emotion
    public enum Emotes { Neutral, Sad, Disgusted, Scared, Terrified, Angry };      //contains all possible facial emotions
    private Emotes currentEmote;            //saves current emotion

    private CharacterData charData;

    private Image spriteImage;
    private Animator spriteAnimator;
    private bool isBlinking;
    private float currentBlinkWaitTime, blinkWaitTime;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        spriteAnimator = GetComponent<Animator>();

        if(charData != null)
            InitializeCharacter();
    }

    /// <summary>
    /// Sets the values of the character data to the component.
    /// </summary>
    private void InitializeCharacter()
    {
        spriteImage.sprite = charData.defaultSprite;
        spriteAnimator.runtimeAnimatorController = charData.characterController;
        currentEmote = Emotes.Neutral;

        if (charData.animateBlink)
        {
            isBlinking = true;
            RandomizeBlinkTime();
        }
        else
            isBlinking = false;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    /// <summary>
    /// Resets all component values to defaults.
    /// </summary>
    private void ResetCharacter()
    {
        if(charData != null)
        {
            spriteImage.sprite = charData.defaultSprite;

            if (charData.animateBlink)
            {
                isBlinking = true;
                RandomizeBlinkTime();
            }
            else
                isBlinking = false;
        }
    }

    private void OnDisable()
    {
        if(charData != null)
        {
            ResetAnimator();
            spriteImage.sprite = charData.defaultSprite;
            currentEmote = Emotes.Neutral;

            if (charData.animateBlink)
                isBlinking = false;
        }
    }

    public void OnShown() => OnEnable();
    public void OnHidden() => OnDisable();

    void Update()
    {
        if(charData != null && spriteAnimator.runtimeAnimatorController != null)
        {
            //If the character can blink, increment the blink timer
            if (charData.animateBlink && isBlinking)
            {
                if (currentBlinkWaitTime > blinkWaitTime)
                    Blink();
                else
                    currentBlinkWaitTime += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Resets the current blink wait time and sets a new random blink wait time.
    /// </summary>
    private void RandomizeBlinkTime()
    {
        currentBlinkWaitTime = 0f;
        Random.InitState(System.DateTime.Now.Millisecond);
        blinkWaitTime = Random.Range(charData.blinkingFrequency.x, charData.blinkingFrequency.y);
    }

    /// <summary>
    /// Sets the blink trigger and re-randomizes the blink time.
    /// </summary>
    private void Blink()
    {
        if(spriteAnimator.runtimeAnimatorController != null)
        {
            spriteAnimator.SetTrigger("isBlink");
            RandomizeBlinkTime();
        }
    }

    /// <summary>
    /// Resets all variables in the sprite animator.
    /// </summary>
    public void ResetAnimator()
    {
        if(spriteAnimator.runtimeAnimatorController != null)
        {
            spriteAnimator.SetBool("isDisgusted", false);
            spriteAnimator.SetBool("isSad", false);
            spriteAnimator.SetBool("isAngry", false);
            spriteAnimator.SetBool("isScared", false);
            spriteAnimator.SetBool("isTerrified", false);
        }
    }

    /// <summary>
    /// Changes the emote of the sprite and plays it.
    /// </summary>
    /// <param name="emotion">The name of the emotion to change to.</param>
    public void ChangeEmote(string emotion)
    {
        emotion = emotion.ToLower();
        switch (emotion)
        {
            case "neutral":
                currentEmote = Emotes.Neutral;
                break;
            case "terrified":
                currentEmote = Emotes.Terrified;
                break;
            case "angry":
                currentEmote = Emotes.Angry;
                break;
            case "disgusted":
                currentEmote = Emotes.Disgusted;
                break;
            case "scared":
                currentEmote = Emotes.Scared;
                break;
            case "sad":
                currentEmote = Emotes.Sad;
                break;
            default:
                Debug.LogWarning("\"" + emotion + "\" does not exist.");
                break;
        }

        //Play the current emote animation
        PlayCurrentEmote();
    }

    /// <summary>
    /// Plays the current emote animation.
    /// </summary>
    public void PlayCurrentEmote()
    {
        if(spriteAnimator.runtimeAnimatorController != null)
        {
            ResetAnimator();

            Debug.Log("Current Emote: " + currentEmote);
            
            switch (currentEmote)
            {
                case Emotes.Neutral:
                    spriteAnimator.Play("Neutral");
                    break;
                case Emotes.Sad:
                    spriteAnimator.SetBool("isSad", true);
                    break;
                case Emotes.Angry:
                    spriteAnimator.SetBool("isAngry", true);
                    break;
                case Emotes.Disgusted:
                    spriteAnimator.SetBool("isDisgusted", true);
                    break;
                case Emotes.Scared:
                    spriteAnimator.SetBool("isScared", true);
                    break;
                case Emotes.Terrified:
                    spriteAnimator.SetBool("isTerrified", true);
                    break;
            }
        }
    }

    public CharacterData GetCharacterData() => charData;
    public Emotes GetCharacterEmote() => currentEmote;

    public void SetCharacterData(CharacterData newCharacterData)
    {
        charData = newCharacterData;
        InitializeCharacter();
    }
}
