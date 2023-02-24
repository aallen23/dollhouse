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
    public List<ObjectData> enableAtPosition;

    private void OnDrawGizmos()
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
