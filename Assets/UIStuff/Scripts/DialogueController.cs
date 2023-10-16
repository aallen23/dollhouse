using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class DialogueController : MonoBehaviour
{
    [SerializeField, Tooltip("The RectTransform of the name tab.")] private RectTransform nameTabTransform;
    [SerializeField, Tooltip("The text for the character name.")] private TextMeshProUGUI characterNameText;

    [SerializeField, Tooltip("The x value of the name tab depending on the position.")] private float nameTabPositionLeft, nameTabPositionCenter, nameTabPositionRight;

    [SerializeField, Tooltip("The sprites shown on the sides of the screen.")] private AnimatedCharacter[] gameSprites;
    [SerializeField, Tooltip("The icon shown on the side of the dialogue box.")] private AnimatedCharacter sideIcon;

    [SerializeField, Tooltip("The container for the options view.")] private Transform optionsViewContainer;

    private DialogueRunner dialogueRunner;

    private bool overrideDialogueComplete;

    public static DialogueController Instance;

    void Awake()
    {
        Instance = this;
        dialogueRunner = GetComponent<DialogueRunner>();
    }

    private void OnEnable()
    {
        dialogueRunner.onNodeStart.AddListener(OnNodeStart);
        dialogueRunner.onNodeComplete.AddListener(OnNodeComplete);
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    private void OnDisable()
    {
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    public void ShowSprite(string spriteName, string direction, bool isActive)
    {
        switch (direction)
        {
            case "left":
                gameSprites[0].ShowCharacter(spriteName, isActive);
                break;
            case "right":
                gameSprites[1].ShowCharacter(spriteName, isActive);
                break;
            default:
                Debug.LogWarning("Direction of type \"" + direction + "\" does not exist.");
                break;
        }
    }

    public void ChangeEmotion(string spriteName, string emotion)
    {
        foreach (var charSprite in gameSprites)
        {
            if (charSprite.GetSpriteAnimator().GetCharacterData() != null && charSprite.GetSpriteAnimator().GetCharacterData().name == spriteName && charSprite.IsShowingSprite())
                charSprite.GetSpriteAnimator().ChangeEmote(emotion);
        }

        if (sideIcon.GetSpriteAnimator().GetCharacterData() != null && sideIcon.GetSpriteAnimator().GetCharacterData().name == spriteName && sideIcon.IsShowingSprite())
            sideIcon.GetSpriteAnimator().ChangeEmote(emotion);
    }

    /// <summary>
    /// Shows or hides an icon on the side of the dialogue box.
    /// </summary>
    /// <param name="spriteName">The name of the icon.</param>
    /// <param name="isActive">If true, the icon is shown. If false, the icon is hidden.</param>
    public void ShowIcon(string spriteName, bool isActive)
    {
        sideIcon.ShowCharacter(spriteName, isActive);
    }

    /// <summary>
    /// Moves the name tab to a specified side of the dialogue box.
    /// </summary>
    /// <param name="direction">The name of the direction to show the name tab on (left, center, or right).</param>
    public void MoveNameTab(string direction)
    {
        Vector3 currentTabPosition = nameTabTransform.anchoredPosition;

        switch (direction)
        {
            case "left":
                currentTabPosition.x = nameTabPositionLeft;
                characterNameText.alignment = TextAlignmentOptions.Left;
                break;
            case "center":
                currentTabPosition.x = nameTabPositionCenter;
                characterNameText.alignment = TextAlignmentOptions.Center;
                break;
            case "right":
                currentTabPosition.x = nameTabPositionRight;
                characterNameText.alignment = TextAlignmentOptions.Right;
                break;
            default:
                Debug.LogWarning("Direction of type \"" + direction + "\" does not exist.");
                break;
        }

        nameTabTransform.anchoredPosition = currentTabPosition;
    }

    /// <summary>
    /// Selects the first option in the Dialogue option view.
    /// </summary>
    public void SelectFirstOption()
    {
        Selectable[] optionButtons = optionsViewContainer.GetComponentsInChildren<Selectable>();
        if (optionButtons.Length > 0)
            EventSystem.current.SetSelectedGameObject(optionButtons[0].gameObject);
    }

    private void Update()
    {
        //Show the character name tab if there is a character name to show
        bool isNameEmpty = string.IsNullOrEmpty(characterNameText.text);
        if (nameTabTransform.gameObject.activeInHierarchy == isNameEmpty)
            nameTabTransform.gameObject.SetActive(!isNameEmpty);
    }

    /// <summary>
    /// Function that is run when the current node is started.
    /// </summary>
    /// <param name="nodeName">The name of the node that was just started.</param>
    private void OnNodeStart(string nodeName)
    {
        switch (nodeName)
        {
            case "Start":
                break;
            default:
                GameManager.Instance.SetCutsceneActive(true);
                break;
        }
    }

    /// <summary>
    /// Function that is run when the current node is completed.
    /// </summary>
    /// <param name="nodeName">The name of the node that was just completed.</param>
    private void OnNodeComplete(string nodeName)
    {
        ResetUI();
        switch (nodeName)
        {
            case "Start":
            case "StartGame":
                overrideDialogueComplete = true;
                break;
            default:
                overrideDialogueComplete = false;
                break;
        }
    }

    /// <summary>
    /// Function that is run when the dialogue is completed.
    /// </summary>
    private void OnDialogueComplete()
    {
        ResetUI();
        if (!overrideDialogueComplete && !FindObjectOfType<MenuManager>().game_ended)
        {
            GameManager.Instance.SetCutsceneActive(false);
        }
    }

    /// <summary>
    /// Resets the UI to its default state.
    /// </summary>
    private void ResetUI()
    {
        //Hide any sprites and icons
        foreach(var charSprite in gameSprites)
            charSprite.ResetCharacter();
        sideIcon.ResetCharacter();

        //Move the tab to the center by default
        MoveNameTab("center");
    }
}
