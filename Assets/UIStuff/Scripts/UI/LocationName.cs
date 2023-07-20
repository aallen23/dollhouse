using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LocationName : MonoBehaviour
{
    [SerializeField, Tooltip("The X position of the location label when hidden.")] private float locationHiddenPos;
    [SerializeField, Tooltip("The X Position of the location label when shown.")] private float locationShowPos;
    [SerializeField, Tooltip("The duration of the location show / hide duration.")] private float locationAniDuration;
    [SerializeField, Tooltip("The ease type for the location show / hide animation.")] private LeanTweenType locationAniEaseType;

    [SerializeField, Tooltip("The duration (in seconds) for the name to fade in / out.")] private float nameFadeDuration;
    [SerializeField, Tooltip("The ease type for the location fade in / out sequence.")] private LeanTweenType nameFadeEaseType;

    private TextMeshProUGUI locationText;
    private bool labelShowing = false;
    private RectTransform labelTransform;
    private CanvasGroup locationTextCanvasGroup;

    private LTDescr currentTween;

    // Start is called before the first frame update
    void Start()
    {
        labelTransform = GetComponent<RectTransform>();
        locationText = GetComponentInChildren<TextMeshProUGUI>();
        locationTextCanvasGroup = GetComponentInChildren<CanvasGroup>();

        Vector3 originalPos = labelTransform.anchoredPosition;
        labelTransform.anchoredPosition = new Vector3(labelShowing ? locationShowPos : locationHiddenPos, originalPos.y, originalPos.z);
        locationTextCanvasGroup.alpha = 0f;
    }

    /// <summary>
    /// Shows or hides the label with a smooth animation.
    /// </summary>
    /// <param name="showLabel">If true, the label is shown. If false, the label is hidden.</param>
    public void ShowLabel(bool showLabel)
    {
        labelShowing = showLabel;
        LeanTween.moveX(labelTransform, labelShowing ? locationShowPos : locationHiddenPos, locationAniDuration).setEase(locationAniEaseType);
    }

    /// <summary>
    /// Changes the location name with an animation.
    /// </summary>
    /// <param name="newName">The name of the most recent location.</param>
    public void ChangeLocationName(string newName)
    {
        //If the new name for the location is already set, return
        if(GameManager.instance != null && GameManager.instance.currentGameLocation == newName)
            return;

        //Show the label if it's hidden
        if (!labelShowing)
            ShowLabel(true);

        if (locationTextCanvasGroup.alpha > 0)
        {
            if (currentTween != null)
                LeanTween.cancel(currentTween.id);
            currentTween = LeanTween.alphaCanvas(locationTextCanvasGroup, 0f, nameFadeDuration).setEase(nameFadeEaseType).setOnComplete(() => UpdateLocationText(newName));
        }
        else
            UpdateLocationText(newName);
    }

    /// <summary>
    /// Updates the text to show the most recent location name.
    /// </summary>
    /// <param name="newName">The name of the most recent location.</param>
    private void UpdateLocationText(string newName)
    {
        locationText.text = newName;
        GameManager.instance.currentGameLocation = newName;
        
        if (currentTween != null)
            LeanTween.cancel(currentTween.id);
        currentTween = LeanTween.alphaCanvas(locationTextCanvasGroup, 1f, nameFadeDuration).setEase(nameFadeEaseType);
    }

    public bool IsLabelShowing() => labelShowing;
}
