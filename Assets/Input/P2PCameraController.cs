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
    private float desiredFOV;
    public float rotationSpeed;
    public float moveSpeed;
    [SerializeField] private ObjectData[] objects;

    public ItemScriptableObject heldItem;

    public DialogueRunner dialog;
    public InventorySystem invSystem;


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

    private void RotRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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

    private void RotLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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

    // Update is called once per frame
    void Update()
    {
        //Vector2 oldMousePos = Mouse.current.position.ReadValue();
        //Vector2 newMousePos = oldMousePos + inputMap.PointToPoint.GamepadMouse.ReadValue<Vector2>();
        //Mouse.current.WarpCursorPosition(newMousePos);


        if (Mouse.current.leftButton.wasPressedThisFrame && dialog.IsDialogueRunning)
        {
            dialog.OnViewRequestedInterrupt();
        }

        gameObject.transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, desiredRotation.x, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.y, desiredRotation.y, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(transform.eulerAngles.z, desiredRotation.z, Time.deltaTime * rotationSpeed)
            );
        //gameObject.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, desiredRotation, Time.deltaTime * rotationSpeed);
        gameObject.transform.position = Vector3.Lerp(transform.position, curPos.transform.position, Time.deltaTime * moveSpeed);

        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());

        ObjectData hitObject;

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.transform.name);
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 5f, 1 << 0) && Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Walk");
                doll.GetComponent<DollBehavior>().od = null;
                Vector3 moveTarget = navPos.position;
                doll.destination = moveTarget;
                //direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                //lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
            }

            hit.transform.gameObject.TryGetComponent(out hitObject);

            if (hitObject)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !dialog.IsDialogueRunning)
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
                if (Mouse.current.leftButton.wasReleasedThisFrame && !dialog.IsDialogueRunning && heldItem)
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
                            invSystem.inv.Remove(heldItem);
                            invSystem.UpdateInventory();
                        }
                    }
                    heldItem = null;
                }
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    heldItem = null;
                }
                
            }
            
        }
        
        foreach (ObjectData od in objects)
        {
            if (od.gameObject != hit.transform.gameObject || dialog.IsDialogueRunning)
            {
                od.gameObject.layer = 0;
            }
            else if (od.gameObject == hit.transform.gameObject)
            {
                hit.transform.gameObject.TryGetComponent(out hitObject);
                if (hitObject)
                {
                    if (hitObject.positionCamera == curPos && hitObject.disableInteractAtPosition)
                    {
                        od.gameObject.layer = 0;
                    }
                    else
                    {
                        od.gameObject.layer = 8;
                    }
                }
            }
        }

        if (Mouse.current.rightButton.isPressed)
        {
            desiredFOV = 30f;
        }
        else
        {
            desiredFOV = 60f;
        }
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, desiredFOV, Time.deltaTime * 4);


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
