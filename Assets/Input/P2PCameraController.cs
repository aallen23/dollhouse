using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Yarn.Unity;
using UnityEngine.EventSystems;

public class P2PCameraController : MonoBehaviour
{
    public Controls inputMap;
    public Vector3 desiredRotation;
    public CameraPosition startPos;
    public CameraPosition curPos;
    private RaycastHit hit;
    public NavMeshAgent doll;
    private float desiredFOV = 60f;
    public float rotationSpeed;
    public float moveSpeed;
    [SerializeField] private ObjectData[] objects;

    public ItemScriptableObject heldItem;

    public DialogueRunner dialog;
    public InventorySystem invSystem;

    public GameObject gamepadMouse;

    public GameObject cursorSprite;


    // Start is called before the first frame update
    void Start()
    {
        objects = FindObjectsOfType<ObjectData>();


        curPos = startPos;
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
        //inputMap.asset.controlSchemes.
    }

    private void Zoom_canceled(InputAction.CallbackContext obj)
    {
        desiredFOV = 60f;
    }

    private void Zoom_performed(InputAction.CallbackContext obj)
    {
        desiredFOV = 30f;
    }

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
        if (!curPos.obeyRotation)
        {
            //Because Move Back
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
        StartMove(i);
    }

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
    
    void StartMove(int i)
    {
        //Debug.Log(i);
        if (curPos.positions[i] != null && !dialog.IsDialogueRunning)
        {
            curPos = curPos.positions[i];
            if (curPos.obeyRotation)
            {
                desiredRotation = curPos.transform.eulerAngles;
            }
            //Debug.Log(curPos.quickSwitch);
            if (curPos.quickSwitch) {
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

    int CalcDirection(int inputDirection)
    {
        int iPos = -1;
        if (!curPos.obeyRotation)
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
        else
        {
            return inputDirection;
        }
    }

    private void RotRight_performed(InputAction.CallbackContext obj)
    {
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation += new Vector3(0f, 90f, 0f);
        }
        else if (curPos.obeyRotation)
        {
            int i = CalcDirection(1);
            StartMove(i);
        }
    }

    private void RotLeft_performed(InputAction.CallbackContext obj)
    {
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation -= new Vector3(0f, 90f, 0f);
        }
        else if (curPos.obeyRotation)
        {
            int i = CalcDirection(3);
            StartMove(i);
        }
    }



    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 0)
        {
            return;
        }
        if (!dialog.IsDialogueRunning)
        {
            hit.transform.gameObject.TryGetComponent(out ObjectData hitObject);
            if (hitObject && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!(hitObject.disableInteractAtPosition && curPos == hitObject.positionCamera))
                {
                    if (hitObject.positionDoll != null)
                    {
                        doll.GetComponent<DollBehavior>().GoToObject(hitObject);
                    }
                    else
                    {
                        hitObject.Interact();
                    }
                }
            }
            else if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 5f, 1 << 0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Walk");
                doll.GetComponent<DollBehavior>().od = null;
                Vector3 moveTarget = navPos.position;
                doll.destination = moveTarget;
                //direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                //lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
            }

        }
        else
        {
            dialog.OnViewRequestedInterrupt();
            return;
        }
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        hit.transform.gameObject.TryGetComponent(out ObjectData hitObject);
        if (hitObject && !dialog.IsDialogueRunning && heldItem)
        {
            Debug.Log("holdingitem");
            if (hitObject.item.Contains(heldItem) && hitObject.itemEnabled)
            {
                Debug.Log("holdingitem2");
                if (hitObject.yarnItem != null && hitObject.yarnItem != "")
                {
                    Debug.Log("holdingitem3");
                    dialog.VariableStorage.SetValue("$itemUsed", hitObject.item.IndexOf(heldItem));
                    dialog.StartDialogue(hitObject.yarnItem);
                }
                else
                {
                    Debug.Log("holdingitem4");
                    hitObject.UseItem(hitObject.item.IndexOf(heldItem));
                }
                if (!heldItem.multiUse)
                {
                    Debug.Log("holdingitem5");
                    invSystem.inv.Remove(heldItem);
                    invSystem.UpdateInventory();
                }
            }
        }
        heldItem = null;
        Destroy(cursorSprite);
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, desiredRotation.x, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.y, desiredRotation.y, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.z, desiredRotation.z, Time.deltaTime * rotationSpeed)
            );
        //gameObject.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, desiredRotation, Time.deltaTime * rotationSpeed);
        gameObject.transform.position = Vector3.Lerp(transform.position, curPos.transform.position, Time.deltaTime * moveSpeed);

        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());
        //Debug.Log(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());
        Physics.Raycast(ray, out hit);
        
        foreach (ObjectData od in objects)
        {
            od.gameObject.layer = 0;

            if (od.gameObject == hit.transform.gameObject && !dialog.IsDialogueRunning && !(od.positionCamera == curPos && od.disableInteractAtPosition) && !EventSystem.current.IsPointerOverGameObject())
            {
                od.gameObject.layer = 8;
            }
        }

        GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, desiredFOV, Time.deltaTime * 4);


        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
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
