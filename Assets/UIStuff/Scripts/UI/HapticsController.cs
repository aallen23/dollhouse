using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HapticsController : MonoBehaviour
{
    public static HapticsController Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Sends a haptic impulse to all connected controllers.
    /// </summary>
    /// <param name="leftMotorAmplitude">The intensity of the haptics on the left side of the controller.</param>
    /// <param name="rightMotorAmplitude">The intensity of the haptics on the right side of the controller.</param>
    /// <param name="seconds">The duration of the haptics event.</param>
    public void PlayControllerHaptics(float leftMotorAmplitude, float rightMotorAmplitude, float seconds)
    {
        if(InputSourceDetector.Instance.currentInputSource == InputSourceDetector.Controls.GAMEPAD)
            StartCoroutine(ControllerHapticsCoroutine(leftMotorAmplitude, rightMotorAmplitude, seconds));
    }

    /// <summary>
    /// Sends a haptic impulse to all controllers.
    /// </summary>
    /// <param name="leftMotorAmplitude">The intensity of the haptics on the left side of the controller.</param>
    /// <param name="rightMotorAmplitude">The intensity of the haptics on the right side of the controller.</param>
    /// <param name="seconds">The duration of the haptics event.</param>
    /// <returns></returns>
    private IEnumerator ControllerHapticsCoroutine(float leftMotorAmplitude, float rightMotorAmplitude, float seconds)
    {
        Gamepad.current.SetMotorSpeeds(leftMotorAmplitude, rightMotorAmplitude);
        yield return new WaitForSecondsRealtime(seconds);
        Gamepad.current.ResetHaptics();
    }
}
