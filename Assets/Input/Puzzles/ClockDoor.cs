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
        Debug.Log(handSmall.transform.eulerAngles + " " + handSmall.transform.localEulerAngles);
        Debug.Log(Mathf.Abs(handBig.transform.localEulerAngles.z - 300f) + " " + Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f));
        //Debug.Log(((int)handBig.desiredlocalEulerAngles.z == (int)compareHandBig.desiredlocalEulerAngles.z) + " " + ((int)handSmall.desiredlocalEulerAngles.z == (int)compareHandSmall.desiredlocalEulerAngles.z));
        if (Mathf.Abs(handBig.transform.localEulerAngles.z - 300f) < 15f && Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f) < 15f)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
