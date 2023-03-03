using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Yarn.Unity;
using UnityEngine.EventSystems;

//Controls the Player character
public class P2PCameraController : MonoBehaviour
{
    private RaycastHit hit;

    [Tooltip("The Input Map we are using.")] public Controls inputMap;
    [Tooltip("Starting CameraPosition.")] public CameraPosition startPos;
    [Tooltip("Current CameraPosition.")] public CameraPosition curPos;
    [Tooltip("The NavMeshAgent of the Doll")] public NavMeshAgent doll;

    [Tooltip("Our Desired Rotation.")] [SerializeField] private Vector3 desiredRotation;
    [Tooltip("Our Desired FOV.")] [SerializeField] float desiredFOV = 60f;

    [Tooltip("The current rotation speed.")] public float rotationSpeed;
    [Tooltip("The current movement speed.")] public float moveSpeed;

    [Tooltip("Array containing all Objects in the scene.")] [SerializeField] private ObjectData[] objects;

    [Tooltip("The Inventory System Controller.")] public InventorySystem invSystem;
    [Tooltip("The currently held Item.")] public ItemScriptableObject heldItem;
    [Tooltip("The displayed sprite of the currently held item, at the cursor position.")] public GameObject cursorSprite;

    [Tooltip("The Dialog System Controller.")] public DialogueRunner dialog;

    [Tooltip("The Cursor for the Gamepad.")] public GameObject gamepadMouse;

    public ObjectData rotateAroundObject;
    private void Awake()
    {
        //Load input map and connect to all the functions
        inputMap = new Controls();
        inputMap.PointToPoint.Enable();
        inputMap.PointToPoint.RotLeft.performed += RotLeft_performed;
        inputMap.PointToPoint.RotRight.performed += RotRight_performed;
        //inputMap.PointToPoint.MoveLeft.performed += MoveLeft_performed;
        //inputMap.PointToPoint.MoveRight.performed += MoveRight_performed;
        inputMap.PointToPoint.MoveForward.performed += MoveForward_performed;
        inputMap.PointToPoint.MoveBackward.performed += MoveBackward_performed;
        inputMap.PointToPoint.Interact.performed += Interact_performed;
        inputMap.PointToPoint.Interact.canceled += Interact_canceled;
        inputMap.PointToPoint.MousePos.performed += MousePos_performed;
        inputMap.PointToPoint.Zoom.performed += Zoom_performed;
        inputMap.PointToPoint.Zoom.canceled += Zoom_canceled;
        inputMap.PointToPoint.Cry.performed += Cry_performed;
        inputMap.PointToPoint.Cry.canceled += Cry_canceled;
    }
    void Start()
    {
        //Load Objects into an array that we can iterate through later.
        objects = FindObjectsOfType<ObjectData>();
        //Move Camera to starting postion (although Doll will likely override) TO FIX
        curPos = startPos;

        
    }

    //Decreases FOV while held (later, we'll Lerp with these values for a smooth transition)
    private void Zoom_performed(InputAction.CallbackContext obj)
    {
        desiredFOV = 30f;
    }

    //Sets desired FOV back to default when button is released
    private void Zoom_canceled(InputAction.CallbackContext obj)
    {
        desiredFOV = 60f;
    }

    //Any time mouse or left analog stick changes positon (moves), we check which device and enable/disable the gamepad cursor as required
    private void MousePos_performed(InputAction.CallbackContext obj)
    {
        if (obj.control.device.name == "Mouse")
        {
            gamepadMouse.SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            gamepadMouse.SetActive(true);
            Cursor.visible = false;
        }
    }

    private void MoveBackward_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection(2);
        if (!curPos.obeyRotation) //Only during BigRoom movement. Can probably be merged into CalcDirection. TO FIX
        {
            //Because we are moving backward, we need to add 2 to the direction
            i += 2;
            if (i > 3)
            {
                i -= 4;
            }
        }
        StartMove(i);
    }

    private void MoveForward_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection(0);
        //Because we are moving forward, we never need to modify i from the default value
        StartMove(i);
    }

    //Commented out, as it was determined that strafing was unneeded. If we change our mind, code will need to be tweaked
    /*private void MoveRight_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection();
        //Because Move Right
        if (i < 3)
        {
            i++;
        }
        else
        {
            i = 0;
        }
        StartMove(i);
    }

    private void MoveLeft_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection();
        //Because Move Left
        if (i > 0)
        {
            i--;
        }
        else
        {
            i = 3;
        }
        StartMove(i);
    }*/
    
    //Actually sets the values we will be Lerping to. Similar to Travel(), and the two should probably be combined. TO FIX
    void StartMove(int i)
    {
        //First we check that there is a valid camera positon in that direction, and we are not mid dialog
        if (curPos.positions[i] != null && !dialog.IsDialogueRunning)
        {
            curPos = curPos.positions[i]; //Update the current camera positon
            if (curPos.obeyRotation)
            {
                //If we must obeyRotation, then we take our desired Rotation from the camera positions rotation
                desiredRotation = curPos.transform.eulerAngles;
            }
            //Debug.Log(curPos.quickSwitch);
            if (curPos.quickSwitch) {
                //If it's a quick switch (so far exclusively inside the Dollhouse, we want the transition to be virtually instant
                rotationSpeed = 256;
                moveSpeed = 256;
            }
            else
            {
                //Otherwise, it's cool to see the camera move a bit.
                rotationSpeed = 16;
                moveSpeed = 16;
            }
        }
    }

   
    private int CalcDirection(int inputDirection)
    {
        int iPos = -1;
        if (!curPos.obeyRotation)  //If we must obey Rotation, we need to check our current rotation, to determine the relative direction we will move
        {
            int choosePos = (int) desiredRotation.y % 360;
            if (choosePos == 0)
            {
                iPos = 0;
            }
            else if (choosePos == 90 || choosePos == -270)
            {
                iPos = 1;
            }
            else if (choosePos == 180 || choosePos == -180)
            {
                iPos = 2;
            }
            else if (choosePos == 270 || choosePos == -90)
            {
                iPos = 3;
            }
            return iPos;
        }
        else //If we aren't, we can just give the input direction right back.
        {
            return inputDirection;
        }
    }

    private void RotRight_performed(InputAction.CallbackContext obj)
    {
        //If we don't have to obey Rotation, then turn right.
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation += new Vector3(0f, 90f, 0f);
        }
        else if (curPos.obeyRotation) //Otherwise, treat it like a strafe
        {
            int i = CalcDirection(1);
            StartMove(i);
        }
    }

    private void RotLeft_performed(InputAction.CallbackContext obj)
    {
        //If we don't have to obey Rotation, then turn left.
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation -= new Vector3(0f, 90f, 0f);
        }
        else if (curPos.obeyRotation) //Otherwise, treat it like a strafe
        {
            int i = CalcDirection(3);
            StartMove(i);
        }
    }
    
    private void Cry_performed(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 0)
        {
            return;
        }
        doll.GetComponent<AudioSource>().Play();
    }



    private void Cry_canceled(InputAction.CallbackContext obj)
    {
        doll.GetComponent<AudioSource>().Stop();
    }


    private void Interact_performed(InputAction.CallbackContext obj)
    {
        //For Gamepad buttons, they trigger _performed when releasing the button (in addition to pressing the button down) with the value of 0. When this occurs, we don't want to do anything, so we return
        if (obj.ReadValue<float>() == 0)
        {
            return;
        }
        if (!dialog.IsDialogueRunning)
        {
            hit.transform.gameObject.TryGetComponent(out ObjectData hitObject);
            if (hitObject && !EventSystem.current.IsPointerOverGameObject()) //First, we check if there is an Object at that positon, and we are not over a UI element
            {
                if (!(hitObject.disableInteractAtPosition && curPos == hitObject.positionCamera)) //Then, we check if the Object is not disabled at our current Position
                {
                    if (hitObject.positionDoll != null) //If that Object has a Doll Positon, we sent Cecilia there. (Later, she'll call Travel())
                    {
                        doll.GetComponent<DollBehavior>().GoToObject(hitObject);
                    }
                    else
                    {
                        hitObject.Interact(); //Otherwise, we Interact() on the object
                    }
                }
            }
            else if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 5f, 1 << 0) && !EventSystem.current.IsPointerOverGameObject()) //If there's no Object, we check if we are clicking on the NavMesh
            {
                //Debug.Log("Walk");
                doll.GetComponent<DollBehavior>().od = null;
                Vector3 moveTarget = navPos.position;
                doll.destination = moveTarget; //we send the Doll there
                //direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                //lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
            }

        }
        else
        {
            //If dialog is running, pressing Interact should continue the dialog
            dialog.OnViewRequestedInterrupt();
            return;
        }
    }

    //When we release the Interact button, we need to do stuff if we're dragging an object
    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        hit.transform.gameObject.TryGetComponent(out ObjectData hitObject);
        if (hitObject && !dialog.IsDialogueRunning && heldItem) //If we are over an Object, not running dialog, and holding an object
        {
            if (hitObject.item.Contains(heldItem) && hitObject.itemEnabled)
            {
                if (hitObject.yarnItem != null && hitObject.yarnItem != "")
                {
                    dialog.VariableStorage.SetValue("$itemUsed", hitObject.item.IndexOf(heldItem));
                    dialog.StartDialogue(hitObject.yarnItem);
                }
                else
                {
                    hitObject.UseItem(hitObject.item.IndexOf(heldItem));
                }
                if (!heldItem.multiUse) 
                {
                    //If the item isn't multi use, like the lantern or any other tool, we need to remove the item and update our inventory display
                    invSystem.inv.Remove(heldItem);
                    invSystem.UpdateInventory();
                }
            }
        }
        //No matter what, we want to remove the item from our cursor
        heldItem = null;
        Destroy(cursorSprite);
        if (rotateAroundObject)
        {
            rotateAroundObject.lookPoint = Vector3.zero;
            rotateAroundObject.GetComponent<ObjectData>().desiredRotation = rotateAroundObject.transform.eulerAngles;
            rotateAroundObject.GetComponent<ObjectData>().functioninteract.Invoke();
            rotateAroundObject = null;
        }
    }

    void Update()
    {

        //Lerping our rotation, the hard way (otherwise, it gets confused when going over 360 and below 0 degrees)
        gameObject.transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, desiredRotation.x, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.y, desiredRotation.y, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.z, desiredRotation.z, Time.deltaTime * rotationSpeed)
            );
        //gameObject.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, desiredRotation, Time.deltaTime * rotationSpeed);
        //Lerping our position to the desired Camera Position at curPos
        gameObject.transform.position = Vector3.Lerp(transform.position, curPos.transform.position, Time.deltaTime * moveSpeed);

        //Shooting out a ray for object interaction
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());
        //Debug.Log(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());
        Physics.Raycast(ray, out hit);
        
        if (rotateAroundObject)
        {
            rotateAroundObject.lookPoint = hit.point;
            rotateAroundObject.mouseDelta = inputMap.PointToPoint.MouseDelta.ReadValue<Vector2>();
        }

        //We need to change object layers if they are interactable, so we can later apply the interact shader based on the layer
        foreach (ObjectData od in objects)
        {
            od.gameObject.layer = 0;

            if (od.gameObject == hit.transform.gameObject && !dialog.IsDialogueRunning && !(od.positionCamera == curPos && od.disableInteractAtPosition) && !EventSystem.current.IsPointerOverGameObject())
            {
                od.gameObject.layer = 8;
            }
        }

        //Lerping our FOV
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, desiredFOV, Time.deltaTime * 4);

        //Eventually will be replaced by our pause system, in the meantime we will want to quit the game this way
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
    //Called by DollBehavior.cs when she reaches her destination (and interacts with an Object) Redudant.
    public void Travel(CameraPosition newPosition)
    {
        curPos = newPosition;
        if (curPos.obeyRotation)
        {
            desiredRotation = curPos.transform.eulerAngles;
        }
        if (curPos.quickSwitch)
        {
            rotationSpeed = 256;
            moveSpeed = 256;
        }
        else
        {
            rotationSpeed = 16;
            moveSpeed = 16;
        }
    }

    
}
