using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    public ObjectData handBig;
    public ObjectData handSmall;

    public ObjectData compareHandBig;
    public ObjectData compareHandSmall;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Check()
    {
        //Debug.Log((int)handBig.desiredRotation.z + " " + (int)compareHandBig.desiredRotation.z + " " + (int)handSmall.desiredRotation.z + " " + (int)compareHandSmall.desiredRotation.z);
        //Debug.Log(((int)handBig.desiredRotation.z == (int)compareHandBig.desiredRotation.z) + " " + ((int)handSmall.desiredRotation.z == (int)compareHandSmall.desiredRotation.z));
        if ((int)handBig.desiredRotation.z == (int)compareHandBig.desiredRotation.z && (int)handSmall.desiredRotation.z == (int)compareHandSmall.desiredRotation.z)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
