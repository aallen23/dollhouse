using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    public Transform handBig;
    public Transform handSmall;

    public Transform compareHandBig;
    public Transform compareHandSmall;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Check()
    {
        //if (handBig.GetComponent<ObjectData>().desiredRotation.z == compareHandBig.localEulerAngles.z && handSmall.GetComponent<ObjectData>().desiredRotation.z == compareHandSmall.localEulerAngles.z)
        if (handBig.localRotation == compareHandBig.localRotation && handSmall.localRotation == compareHandSmall.localRotation)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
