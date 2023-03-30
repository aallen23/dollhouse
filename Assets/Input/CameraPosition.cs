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

    [Tooltip("GameObjects to show at this position, and hide otherwise.")]
    public List<GameObject> enableAtPosition;

    [Tooltip("What GameObject to rotate around.")]
    public GameObject rotateAround;

    private void Start()
    {
        if (enableAtPosition.Count > 0)
        {
            foreach (GameObject obj in enableAtPosition)
            {
                obj.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (rotateAround)
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.up, 10 * Time.deltaTime);
            transform.LookAt(rotateAround.transform);
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
