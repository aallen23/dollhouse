using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A position the Player Camera can travel to
public class CameraPosition : MonoBehaviour
{
    [Tooltip("Do in order: Z+, X+, Z-, X-")] //Might not be accurate
    public CameraPosition[] positions;
    [Tooltip("Should this object's rotation be used instead of the Camera's?")]
    public bool obeyRotation;
    [Tooltip("Should the Camera move here quickly?")]
    public bool quickSwitch;
	//[Tooltip("Should the Camera move away smoothly?")]
	//public bool forceSmooth;
	public float houseYRotation;

    [Tooltip("GameObjects to show at this position, and hide otherwise.")]
    public List<GameObject> enableAtPosition;

	[Tooltip("AudioSources to enable at this position, and hide otherwise.")]
	public List<AudioSource> audioAtPosition;
	public bool playFromStart;

    [Tooltip("What GameObject to rotate around.")]
    public GameObject rotateAround;

	public string runYarn;
	public bool runYarnOnce;
	public bool ranYarn;

	public bool inDollhouse;

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
        if (inDollhouse != GameManager.instance.inDollhouse)
        {
            GameManager.instance.inDollhouse = inDollhouse;
            GameManager.instance.OnSwitchPerspective();
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
