using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Yarn.Unity;


public enum InteractType
{
    Examine,
    Rotate,
    Teleport
}

public enum ObjectUseType
{
    None,
    ShowObject
}

public class ObjectData : MonoBehaviour
{
    [Tooltip("If true, Doll must travel to object before Interact()ing.")]
    public bool isDollObject;
    [Tooltip("Move Doll to here.")]
    public Transform positionDoll;

    [Tooltip("CameraPosition to LERP camera to when Interact()ing.")]
    public CameraPosition positionCamera;
    [Tooltip("If True, object is not interactable while at the above CameraPosition.")]
    public bool disableInteractAtPosition;


    [Header("Clicking Settings")]
    [Tooltip("What happens when this object is clicked.")]
    public InteractType interactType;

    [Space(20)]
    [Tooltip("Yarn node to call when Examined.")]
    public string yarnExamine;

    [Space(10)]
    [Tooltip("For Rotate type objects, apply the following rotation:")]
    public Vector3 rotateAmount;
    [Tooltip("For Rotate type objects, apply rotation to this object.")]
    public GameObject rotateObject;

    [Space(10)]
    [Tooltip("For Teleport type objects, teleport to this point.")]
    public Transform teleportPoint;


    [Tooltip("What function to call, if any.")]
    public UnityEvent functioninteract;


    [Header("Item Interaction Settings")]
    [Tooltip("What ItemScriptableObject can be used on this object.")]
    public ItemScriptableObject item;

    [Tooltip("What happens when an Item is dragged onto this object.")]
    public ObjectUseType objectUseType;
    [Tooltip("Yarn node to call (if any) when the Item is dragged onto this object.")]
    public string yarnItem;

    [Space(20)]
    [Tooltip("For ShowObject Type objects, show this GameObject.")]
    public GameObject shownObject;

    //Private variables for calling fucntions
    private DialogueRunner dialog;
    private P2PCameraController player;

    void Start()
    {
        //Find specific GameObjects to be called alter.
        dialog = FindObjectOfType<DialogueRunner>();
        player = FindObjectOfType<P2PCameraController>();


        //If an Item would show an GameObject, we want it to start hidden
        StartCoroutine(HideShownObject());
    }

    IEnumerator HideShownObject()
    {
        yield return new WaitForSeconds(0.5f);
        if (shownObject)
        {
            shownObject.SetActive(false);
        }
    }

    //Function
    public void Interact()
    {
        if (positionCamera)
        {
            player.Travel(positionCamera);
        }

        switch (interactType)
        {
            case InteractType.Examine:
                dialog.StartDialogue(yarnExamine);
                break;
            case InteractType.Rotate:
                rotateObject.transform.Rotate(rotateAmount, Space.Self);
                break;
            case InteractType.Teleport:
                NavMeshAgent doll = FindObjectOfType<DollBehavior>().GetComponent<NavMeshAgent>();
                doll.Warp(teleportPoint.position);
                break;
        }
        functioninteract.Invoke();
    }


    [YarnCommand("use_item")]
    public void UseItem()
    {
        switch (objectUseType)
        {
            case ObjectUseType.ShowObject:
                shownObject.SetActive(true);
                break;
        }
        
    }
}
