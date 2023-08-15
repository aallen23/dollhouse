using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintManager : MonoBehaviour
{
    [SerializeField, Tooltip("The button component for the hint button.")] private Button hintButton;
    [SerializeField, Tooltip("The text used to display the most recent hint.")] private TextMeshProUGUI hintText;

    [SerializeField, Tooltip("The X position of the UI when hidden.")] private float hintUIStartPos;
    [SerializeField, Tooltip("The X position of the UI when shown.")] private float hintUIEndPos;
    [SerializeField, Tooltip("The duration in seconds of the hint UI slide animation.")] private float hintUIAniDuration;
    [SerializeField, Tooltip("The ease type for the hint UI showing.")] private LeanTweenType hintUIShowEaseType;
    [SerializeField, Tooltip("The ease type for the hint UI hiding.")] private LeanTweenType hintUIHideEaseType;

    private RectTransform hintRectTransform;
    private bool animationActive;
    private bool hintButtonShowing;
    private bool hintMenuActive;

    private Controls playerControls;

    private void Awake()
    {
        playerControls = new Controls();
        playerControls.Menu.Hint.started += _ => HintHotkey();
        playerControls.Menu.Cancel.started += _ => OnCancel();
    }

    // Start is called before the first frame update
    void Start()
    {
        hintRectTransform = GetComponent<RectTransform>();
        ResetHintUI();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    /// <summary>
    /// Resets the text and the position of the hint UI.
    /// </summary>
    private void ResetHintUI()
    {
        hintText.text = "";
        hintRectTransform.anchoredPosition = new Vector2(hintUIStartPos, hintRectTransform.anchoredPosition.y);
        hintMenuActive = false;
        UpdateHintButton();
    }

    /// <summary>
    /// Function called when the hint hotkey button is pressed.
    /// </summary>
    private void HintHotkey()
    {
        if (GameManager.Instance.IsGameplayActive())
        {
            if(hintMenuActive)
                HideHint();
            else
                ShowHint();
        }
    }

    /// <summary>
    /// Function called when the cancel button is pressed.
    /// </summary>
    private void OnCancel()
    {
        if (hintMenuActive)
            HideHint();
    }

    /// <summary>
    /// Shows a hint from the list of active quests.
    /// </summary>
    public void ShowHint()
    {
        string hint = QuestManager.Instance.GetActiveQuestHint();

        //If there is a hint to show, update the text and show the hint UI.
        if(hint != null)
        {
            hintText.text = hint;
            ShowHintUI(true);
        }
    }

    /// <summary>
    /// Hides the hint from the UI.
    /// </summary>
    public void HideHint()
    {
        ShowHintUI(false);
    }

    /// <summary>
    /// Shows or hides the hint UI.
    /// </summary>
    /// <param name="showUI">If true, the hint UI is active.</param>
    private void ShowHintUI(bool showUI)
    {
        //Do not animate the UI if it's already in the middle of an animation
        if (animationActive)
            return;

        hintMenuActive = showUI;

        animationActive = true;
        LeanTween.moveX(hintRectTransform, showUI ? hintUIEndPos : hintUIStartPos, hintUIAniDuration).setEase(showUI ? hintUIShowEaseType : hintUIHideEaseType).setOnComplete(() => animationActive = false);
    }

    private void Update()
    {
        int activeQuestNumber = QuestManager.Instance.GetActiveQuestNumber();

        if (!hintButtonShowing && activeQuestNumber > 0)
        {
            hintButtonShowing = true;
            hintButton.interactable = true;
            UpdateHintButton();
        }

        else if(hintButtonShowing && activeQuestNumber == 0)
        {
            hintButtonShowing = false;
            hintButton.interactable = false;
            UpdateHintButton();
        }

    }

    /// <summary>
    /// Updates the hint button image to display / hide depending on whether a hint can be displayed.
    /// </summary>
    private void UpdateHintButton()
    {
        if (hintButton != null)
        {
            CanvasGroup buttonCanvas = hintButton.GetComponent<CanvasGroup>();
            buttonCanvas.alpha = hintButtonShowing ? 1 : 0;
        }

        else
            Debug.LogWarning("Warning: Hint Button Not Set In Inspector.");
    }
}
