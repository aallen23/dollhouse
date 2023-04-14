using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Switches the Player Camera when the Doll enters the Trigger
public class CameraSwitcher : MonoBehaviour
{
    private P2PCameraController pcamera;
    private DollBehavior doll;

    private void Start()
    {
        pcamera = FindObjectOfType<P2PCameraController>();
        doll = FindObjectOfType<DollBehavior>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject == doll.gameObject && pcamera.gameStarted)
        {
            pcamera.Travel(GetComponentInParent<CameraPosition>());
            doll.dollCamera.transform.position = GetComponentInParent<CameraPosition>().gameObject.transform.position;
            doll.dollCamera.transform.rotation = GetComponentInParent<CameraPosition>().gameObject.transform.rotation;
        }
        else if (other.gameObject == doll.gameObject) //Delete if you want the camera to start not in dollhouse (mostly)
        {
            doll.dollCamera.transform.position = GetComponentInParent<CameraPosition>().gameObject.transform.position;
            doll.dollCamera.transform.rotation = GetComponentInParent<CameraPosition>().gameObject.transform.rotation;
        }

    }
}
