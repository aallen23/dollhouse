using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooking : MonoBehaviour
{
    private P2PCameraController playerCam;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = FindObjectOfType<P2PCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(new Vector3(playerCam.transform.position.x, playerCam.transform.position.y, -playerCam.transform.position.z));
        //Debug.Log("hmm");
    }
}
