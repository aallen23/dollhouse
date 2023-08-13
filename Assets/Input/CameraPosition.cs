using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A position the Player Camera can travel to
public class CameraPosition : MonoBehaviour
{
    [Tooltip("Possible Camera Positions that can be traveled to. For Big Room, should be in this order: Z+, X+, Z-, X-")] public CameraPosition[] positions;
    [Tooltip("Should this object's rotation be used instead of the Camera's?")] public bool obeyRotation;
    [Tooltip("Should the Camera move here quickly?")] public bool quickSwitch;
	[Tooltip("For Big Room movement, what Y Rotation should we snap the camera to when leaving this position?")] public float houseYRotation;

    [Tooltip("GameObjects to show at this position, and hide otherwise.")] public List<GameObject> enableAtPosition;

	[Tooltip("AudioSources to enable at this position, and hide otherwise.")] public List<AudioSource> audioAtPosition;
	[Tooltip("Should the AudioSources play at the start of the game?")] public bool playFromStart;

    [Tooltip("What GameObject should we continually rotate around.")] public GameObject rotateAround;

	//Below is currently only used for the intial Cece encounter, but could be used elsewhere as well
	[Tooltip("When arriving, what Yarn passage should we run?")] public string runYarn;
	[Tooltip("If True, we only run the Yarn passage the first time we arrive.")] public bool runYarnOnce;
	[Tooltip("Have we run the Yarn passage yet?")] public bool ranYarn;

	[Tooltip("Is this Camera Position in the Dollhouse?")] public bool inDollhouse;
    [Tooltip("Enable the Gamepad Cursor?")] public bool enableCursorForGamepad = true;

    private P2PCameraController player;

    private void Start()
    {
		player = FindObjectOfType<P2PCameraController>();
        if (enableAtPosition.Count > 0)
        {
            foreach (GameObject obj in enableAtPosition)
            {
                obj.SetActive(false);
            }
        }
        /*if (audioAtPosition.Count > 0)
        {
            foreach (AudioSource obj in audioAtPosition)
            {
                obj.enabled = false;
            }
        }*/
    }

    private void Update()
    {
        if (rotateAround && !player.gameStarted)
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.up, 10 * Time.deltaTime);
            transform.LookAt(rotateAround.transform);
        }
    }

    /// <summary>
    /// Checks to see if the player is in or outside of the Doll House.
    /// </summary>
    public void OnPositionMovement()
    {
        //If the camera position boolean is different than what the game has stored, change the variable
        if (inDollhouse != GameManager.Instance.inDollhouse)
        {
            GameManager.Instance.inDollhouse = inDollhouse;
            GameManager.Instance.OnSwitchPerspective();
        }

        //If the enable gamepad cursor boolean is different than what the game has stored, change the variable
        if(enableCursorForGamepad != GameManager.Instance.gamepadCursorActive)
        {
            GameManager.Instance.SetGamepadCursorActive(enableCursorForGamepad);
        }
    }

    private void OnDrawGizmos()
    {

        if (gameObject.name == "DollCameraPos")
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 6f);
        }
        else
        {

            if (obeyRotation)
            {
                if (quickSwitch)
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.green;
                }
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(transform.position, 5f);
        }
    }
}
