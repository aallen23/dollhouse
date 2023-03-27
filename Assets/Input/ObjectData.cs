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
    RotateAround,
    Teleport,
    AddItem,
    BlankHand
}

public enum ObjectUseType
{
    None,
    ShowObject,
    AddItem,
    AddItemElsewhere
}

//Stores an Interactable Object's Data
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
    public Vector3 desiredRotation; //We'll change this, so we can lerp the rotation nicely.
    public float rotationSpeed;
    [Tooltip("For Rotate type objects, apply rotation to this object.")]
    public ObjectData rotateObject;

    [Space(10)]
    [Tooltip("For Teleport type objects, teleport to this point.")]
    public Transform teleportPoint;

    [Space(10)]
    [Tooltip("For AddItem type objects, add this item to your inventory")]
    public ItemScriptableObject addedItem;
    [Tooltip("For AddItem type objects, is this multiUse or one-time only?")]
    public ItemScriptableObject addItemIsInfinite;
    [Tooltip("(Optional) For AddItem type objects, hide this GameObject.")]
    public GameObject addItemHideObject;
    [Tooltip("(Optional) For AddItem type objects, toggle Item Enabled on this object after adding.")]
    public ObjectData itemEnabledToggleObject;

    [Tooltip("(Optional) What function to call after interacting.")]
    public UnityEvent functioninteract;


    [Header("Item Interaction Settings")]
    [Tooltip("What ItemScriptableObject can be used on this object.")]
    public List<ItemScriptableObject> item;

    [Tooltip("Are Item Interactions enabled?")]
    public bool itemEnabled;

    [Tooltip("What happens when an Item is dragged onto this object.")]
    public ObjectUseType objectUseType;
    [Tooltip("Yarn node to call (if any) when the Item is dragged onto this object.")]
    public string yarnItem;

    [Space(20)]
    [Tooltip("For ShowObject Type objects, show this GameObject.")]
    public GameObject shownObject;
    [Tooltip("For ShowObject Type objects, modify the color.")]
    public bool shownObjectModColor;
    [Tooltip("For ShowObject Type objects, modify the Add Item Hide Object value with the Item. (Useful for dropping an item, or similar)")]
    public bool shownObjectItemOverride;
    [Tooltip("For ShowObject Type objects, should Shown Object start visible?")]
    public bool startVisible;

    public ItemScriptableObject itemAddItem;

    public ObjectData addItemDestination;
    public ItemScriptableObject addItemItem;

    [Tooltip("(Optional) What function to call after using an Item")]
    public UnityEvent functionItem;

    //Private variables for calling fucntions
    private DialogueRunner dialog;
    private P2PCameraController player;
    public Vector3 lookPoint;
    public Vector2 mouseDelta;
    public Vector3 rotateAroundModAngle;
    public Vector3 rotateAroundForceAngle;
    private Vector3 defaultPos;
    public Vector3 desiredPos;
    public Vector3 secondPos;
    public float animSpeed;
    public AudioSource interactSFX;

    void Start()
    {
        defaultPos = transform.localPosition;
        desiredPos = defaultPos;
        //Find specific GameObjects to be called alter.
        dialog = FindObjectOfType<DialogueRunner>();
        player = FindObjectOfType<P2PCameraController>();
        desiredRotation = transform.eulerAngles;

        //If an Item would show an GameObject, we want it to start hidden
        if (!startVisible)
        {
            StartCoroutine(HideShownObject());
        }
        if (positionDoll)
        {
            if (positionDoll.TryGetComponent(out MeshRenderer mesh))
            {
                mesh.enabled = false;
            }
        }
        if (teleportPoint)
        {
            if (teleportPoint.TryGetComponent(out MeshRenderer mesh))
            {
                mesh.enabled = false;
            }
        }
    }

    IEnumerator HideShownObject()
    {
        yield return new WaitForSeconds(0.5f);
        if (shownObject)
        {
            shownObject.SetActive(false);
        }
    }

    void Update()
    {
        //Rotates object (uses Quaternion.Lerp isntead of below to avoid Gimbal Lock) :)
        if (lookPoint == Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRotation), Time.deltaTime * rotationSpeed);
        }
        else
        {
            //lookPoint.y = 0;
            //transform.LookAt(new Vector3(lookPoint.x, lookPoint.y, transform.position.z));
            Vector3 dir = lookPoint - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotationQ = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.back));
            transform.rotation = new Quaternion(0, 0, rotationQ.z, rotationQ.w);
            //transform.eulerAngles += rotateAroundModAngle;
            //Debug.Log(transform.eulerAngles.z);
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
            //Debug.Log(transform.eulerAngles.z);
        }

        if (transform.localPosition != desiredPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, Time.deltaTime * animSpeed) ;
        }
        else if (desiredPos != defaultPos)
        {
            desiredPos = defaultPos;
        }

        /*transform.localEulerAngles = new Vector3(
             Mathf.LerpAngle(transform.localEulerAngles.x, desiredRotation.x, Time.deltaTime * rotationSpeed),
             Mathf.LerpAngle(transform.localEulerAngles.y, desiredRotation.y, Time.deltaTime * rotationSpeed),
             Mathf.LerpAngle(transform.localEulerAngles.z, desiredRotation.z, Time.deltaTime * rotationSpeed)
             );*/
    }

    //Based on InteractType, we do various things
    public void Interact()
    {
        //If it has a camera position, go there first.
        if (positionCamera)
        {
            player.Travel(positionCamera);
        }

        if (interactSFX)
        {
            interactSFX.Play();
        }

        switch (interactType)
        {
            case InteractType.Examine: 
                if (yarnExamine != "" && yarnExamine != null)
                {
                    dialog.StartDialogue(yarnExamine); //Trigger yarn
                }
                break;
            case InteractType.Rotate:
                rotateObject.desiredRotation += rotateAmount;
                //rotateObject.transform.Rotate(rotateAmount, Space.Self);
                break;
            case InteractType.RotateAround:
                player.rotateAroundObject = this;
                break;
            case InteractType.Teleport:
                NavMeshAgent doll = FindObjectOfType<DollBehavior>().GetComponent<NavMeshAgent>();
                doll.Warp(teleportPoint.position);
                break;
            case InteractType.AddItem:
                InventorySystem inv = FindObjectOfType<InventorySystem>();
                inv.inv.Add(addedItem);
                inv.UpdateInventory();
                if (addItemHideObject)
                {
                    addItemHideObject.SetActive(false);
                }
                if (itemEnabledToggleObject)
                {
                    itemEnabledToggleObject.itemEnabled = !itemEnabledToggleObject.itemEnabled;
                }
                if (!addItemIsInfinite)
                {
                    addedItem = null;
                }
                if (yarnExamine != "" && yarnExamine != null)
                {
                    dialog.StartDialogue(yarnExamine); //Trigger yarn
                }
                break;
        }
        if (secondPos != Vector3.zero || secondPos != null)
        {
            desiredPos = defaultPos + secondPos;
        }
        functioninteract.Invoke();
    }


    [YarnCommand("use_item")]
    public void UseItem(float i)
    {
        int it = (int)i;
        switch (objectUseType)
        {
            case ObjectUseType.ShowObject:
                shownObject.SetActive(true);
                if (shownObjectModColor)
                {
                    shownObject.GetComponent<Renderer>().material = item[it].displayMaterial;
                    
                }
                shownObject.TryGetComponent(out ObjectData data);
                if (data && shownObjectItemOverride)
                {
                    data.addedItem = item[it];
                }
                if (itemEnabled)
                {
                    itemEnabled = !itemEnabled;
                }
                break;
            case ObjectUseType.AddItem:
                InventorySystem inv = FindObjectOfType<InventorySystem>();
                inv.inv.Add(itemAddItem);
                inv.UpdateInventory();
                if (itemEnabled)
                {
                    itemEnabled = !itemEnabled;
                }
                break;
            case ObjectUseType.AddItemElsewhere:
                addItemDestination.addedItem = addItemItem;
                break;
        }
        functionItem.Invoke();
        
    }
}
