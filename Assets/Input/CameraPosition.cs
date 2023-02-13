using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [Tooltip("Do in order: Z+, X+, Z-, X-")]
    public CameraPosition[] positions;
    public bool obeyRotation;
    public bool quickSwitch;

    public void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
