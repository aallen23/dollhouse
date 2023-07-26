using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField, Tooltip("The container for the credits that scrolls.")] private RectTransform creditsText;
    [SerializeField, Tooltip("The animation curve that controls the smoothness of the credit scroll.")] private AnimationCurve creditsAnimCurve;
    [SerializeField, Tooltip("The amount of time (in seconds) for the credits to scroll from beginning to end.")] private float scrollTime;
    [SerializeField, Tooltip("The amount of time (in seconds) that has passed in the credits scroll.")] private float timeElapsed;
    [SerializeField, Tooltip("The multiplier for the speed of the credits scroll when fast forward is active.")] private float fastForwardMultiplier;
    [SerializeField, Tooltip("The fast forward icon indicating that fast forward is active.")] private GameObject fastForwardIcon;

    private Vector3 creditsStartPos, creditsEndPos;
    private bool isFastForwarding;
    private Controls inputControls;

    public void Awake()
    {
        creditsStartPos = creditsText.anchoredPosition;
        creditsEndPos = new Vector3(creditsStartPos.x, creditsStartPos.y + creditsText.sizeDelta.y, creditsStartPos.z);

        //Binds the toggle fast forward function to the fast forward input action
        inputControls = new Controls();
        inputControls.Menu.FastForward.performed += _ => ToggleFastForward();
        inputControls.Menu.FastForward.canceled += _ => ToggleFastForward();
    }

    private void OnEnable()
    {
        inputControls.Enable();
        ResetScroll();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    /// <summary>
    /// Starts the credits scroll coroutine.
    /// </summary>
    public void StartScroll()
    {
        ResetScroll();
        StartCoroutine(Scroll());
    }

    /// <summary>
    /// Toggles the fast forward variable and the visibility of the fast forward icon.
    /// </summary>
    private void ToggleFastForward()
    {
        isFastForwarding = !isFastForwarding;
        fastForwardIcon.SetActive(isFastForwarding);
    }

    /// <summary>
    /// Smoothly scrolls the credits from start to finish.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Scroll()
    {
        while(timeElapsed < scrollTime)
        {
            //Calculate the position of the credits based on the animation curve
            float t = creditsAnimCurve.Evaluate(timeElapsed / scrollTime);

            creditsText.anchoredPosition = Vector3.Lerp(creditsStartPos, creditsEndPos, t);

            //Increment time based on the scroll speed
            timeElapsed += Time.deltaTime * ScrollSpeedMultiplier();
            yield return null;
        }

        OnCreditsEnd();
    }

    /// <summary>
    /// Function called when the credits finish scrolling.
    /// </summary>
    public void OnCreditsEnd()
    {
        //This function can be used to activate any sequences once the credits have finished scrolling
    }

    /// <summary>
    /// Resets the scroll and time position of the credits.
    /// </summary>
    public void ResetScroll()
    {
        creditsText.anchoredPosition = creditsStartPos;
        timeElapsed = 0.0f;
        isFastForwarding = false;
        fastForwardIcon.SetActive(isFastForwarding);
    }

    /// <summary>
    /// Gets the current scroll multiplier for the scroll speed.
    /// </summary>
    /// <returns>Returns 1 if fast forward is inactive or the fast forward multiplier if it is active.</returns>
    private float ScrollSpeedMultiplier() => isFastForwarding ? fastForwardMultiplier : 1;
}
