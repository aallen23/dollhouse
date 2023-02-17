using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooking : MonoBehaviour
{
    public Transform playerCam;
    private GameObject mirror;

    // Start is called before the first frame update
    void Start()
    {
        //playerCam = FindObjectOfType<P2PCameraController>();
        mirror = GameObject.Find("Mirror");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = (playerCam.transform.position + mirror.transform.position) / 2;
        //Debug.Log("hmm");
    }
}
