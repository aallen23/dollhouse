using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChanger : MonoBehaviour
{
    [SerializeField, Tooltip("The name of the new location when entering volume.")] private string locationName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cece"))
            FindObjectOfType<LocationName>().ChangeLocationName(locationName);
    }
}
