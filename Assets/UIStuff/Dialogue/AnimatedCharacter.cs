using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class AnimatedCharacter : MonoBehaviour
{
    [SerializeField, Tooltip("A list of character data.")] private CharacterData[] characterData;

    private SpriteAnimator spriteAnimator;
    private CanvasGroup canvasGroup;
    private bool isSpriteShowing;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        spriteAnimator = GetComponentInChildren<SpriteAnimator>();

        canvasGroup.alpha = 0;
        isSpriteShowing = false;
    }

    /// <summary>
    /// Shows or hides the character's sprite.
    /// </summary>
    /// <param name="spriteName">The name of the character.</param>
    /// <param name="showSprite">If true, the sprite is shown. If false, the sprite is hidden.</param>
    public void ShowCharacter(string spriteName, bool showSprite)
    {
        if (!showSprite)
        {
            HideSprite(true);
            return;
        }

        //Find the character in the list of character data
        CharacterData characterData = FindCharacter(spriteName);


        if(characterData == null)
            Debug.LogWarning("\"" + spriteName + "\" does not exist.");

        //If the sprite animator does not have character data or the character data is different from the shown character, set the new character data
        else if(spriteAnimator.GetCharacterData() == null || characterData.name != spriteAnimator.GetCharacterData().name)
            spriteAnimator.SetCharacterData(characterData);

        ShowSprite(true);
    }

    /// <summary>
    /// Shows the sprite in the game.
    /// </summary>
    /// <param name="isAnimated">If true, the entrance of the sprite is animated.</param>
    private void ShowSprite(bool isAnimated)
    {
        if (isAnimated && !isSpriteShowing)
        {
            //This function can be used for animation
            canvasGroup.alpha = 1;
            isSpriteShowing = true;
            spriteAnimator.OnShown();
        }
        else
        {
            canvasGroup.alpha = 1;
            isSpriteShowing = true;
            spriteAnimator.OnShown();
        }
    }

    /// <summary>
    /// Hides the sprite in the game.
    /// </summary>
    /// <param name="isAnimated">If true, the exit of the sprite is animated.</param>
    private void HideSprite(bool isAnimated)
    {
        if (isAnimated && isSpriteShowing)
        {
            //This function can be used for animation
            canvasGroup.alpha = 0;
            isSpriteShowing = false;
            spriteAnimator.OnHidden();
        }
        else
        {
            canvasGroup.alpha = 0;
            isSpriteShowing = false;
            spriteAnimator.OnHidden();
        }
    }

    /// <summary>
    /// Hides the characters without any animations.
    /// </summary>
    public void ResetCharacter()
    {
        HideSprite(false);
    }

    /// <summary>
    /// Finds the characters in the list of character data by their character names.
    /// </summary>
    /// <param name="characterName">The name of the character.</param>
    /// <returns>If no character is found, returns null. If found, the data for the character is returned.</returns>
    private CharacterData FindCharacter(string characterName)
    {
        return System.Array.Find(characterData, character => character.name == characterName);
    }

    public SpriteAnimator GetSpriteAnimator() => spriteAnimator;
    public bool IsShowingSprite() => isSpriteShowing;
}
