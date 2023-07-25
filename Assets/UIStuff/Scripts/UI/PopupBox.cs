using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupBox : MonoBehaviour
{
    [SerializeField, Tooltip("The position of the popup label when hidden.")] private Vector2 popupStartPos;
    [SerializeField, Tooltip("The Position of the popup label when shown.")] private Vector2 popupEndPos;
    [SerializeField, Tooltip("The duration of the popup entrance duration.")] private float popupStartAniDuration;
    [SerializeField, Tooltip("The duration of the wait time between the entrance and exit of the popup box.")] private float popupExitDelay;
    [SerializeField, Tooltip("The duration of the popup exit duration.")] private float popupExitAniDuration;
    [SerializeField, Tooltip("The ease type for the popup start animation.")] private LeanTweenType popupStartAniEaseType;
    [SerializeField, Tooltip("The ease type for the popup end animation.")] private LeanTweenType popupExitAniEaseType;

    [SerializeField, Tooltip("The duration (in seconds) for the popup box to fade in.")] private float fadeDuration;
    [SerializeField, Tooltip("The ease type for the location fade in / out sequence.")] private LeanTweenType fadeEaseType;

    private RectTransform popupBox;
    private CanvasGroup popupBoxCanvasGroup;
    private Vector3 popupBoxStartPos;
    private TextMeshProUGUI popupBoxText;

    private void Start()
    {
        popupBoxCanvasGroup = GetComponentInChildren<CanvasGroup>();
        popupBox = popupBoxCanvasGroup.GetComponent<RectTransform>();
        popupBoxStartPos = popupBox.anchoredPosition;
        popupBoxText = popupBox.GetComponentInChildren<TextMeshProUGUI>();
        ResetPopup();
    }

    private void ResetPopup()
    {
        popupBoxCanvasGroup.alpha = 0f;
        popupBox.anchoredPosition = popupBoxStartPos;
        LeanTween.cancel(gameObject);
    }

    public void ShowPopup(string popupText)
    {
        ResetPopup();
        popupBoxText.text = popupText;

        LeanTween.move(popupBox, popupEndPos, popupStartAniDuration).setEase(popupStartAniEaseType).setOnComplete(() => LeanTween.delayedCall(popupExitDelay, () => LeanTween.move(popupBox, popupStartPos, popupExitAniDuration).setEase(popupExitAniEaseType)));
        LeanTween.alphaCanvas(popupBoxCanvasGroup, 1f, fadeDuration).setEase(fadeEaseType);
    }
}
