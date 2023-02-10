using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ObjectData : MonoBehaviour
{
    [Tooltip("The Yarn Node to run when object clicked.")]
    public string yarnNode;
    [Tooltip("Move camera to this CameraPosition when clicked. If rotation desired, set Obey Rotation to true on this object.")]
    public CameraPosition moveToHere;
    [Tooltip("Move Doll to here.")]
    public Transform dollToHere;

    [Tooltip("If True, object is not interactable while at the above position.")]
    public bool notSelectableWhenHere;
    public GameObject objectToApplyRotationTo;
    [Tooltip("Apply the following rotation")]
    public Vector3 rotationToApply;

    public ItemScriptableObject applyableItem;
    public string yarnNodeItem;
    public GameObject objectToShowWithItem;


    // Start is called before the first frame update
    void Start()
    {
        if (objectToShowWithItem != null)
        {
            objectToShowWithItem.SetActive(false);
        }
    }

    [YarnCommand("use_item")]
    public void UseItem()
    {
        objectToShowWithItem.SetActive(true);
        
    }
}
