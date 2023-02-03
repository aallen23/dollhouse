using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Yarn.Unity;

public class P2PCameraController : MonoBehaviour
{
    public Controls inputMap;
    public int desiredRotation;
    public CameraPosition startPos;
    public CameraPosition curPos;
    private RaycastHit hit;
    public NavMeshAgent doll;
    private float desiredFOV;
    public float rotationSpeed;
    public float moveSpeed;
    public List<GameObject> objects;

    public DialogueRunner dialog;

    // Start is called before the first frame update
    void Start()
    {
        curPos = startPos;
        desiredRotation = 0;
        inputMap = new Controls();
        inputMap.PointToPoint.Enable();
        inputMap.PointToPoint.RotLeft.performed += RotLeft_performed;
        inputMap.PointToPoint.RotRight.performed += RotRight_performed;
        inputMap.PointToPoint.MoveLeft.performed += MoveLeft_performed;
        inputMap.PointToPoint.MoveRight.performed += MoveRight_performed;
        inputMap.PointToPoint.MoveForward.performed += MoveForward_performed;
        inputMap.PointToPoint.MoveBackward.performed += MoveBackward_performed;
    }

    private void MoveBackward_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection();
        //Because Move Back
        i += 2;
        if (i > 3)
        {
            i -= 4;
        }
        StartMove(i);
    }

    private void MoveForward_performed(InputAction.CallbackContext obj)
    {
        int i = CalcDirection();
        StartMove(i);
    }

    private void MoveRight_performed(InputAction.CallbackContext obj)
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
    }
    
    void StartMove(int i)
    {
        //Debug.Log(i);
        if (curPos.positions[i] != null && !dialog.IsDialogueRunning)
        {
            curPos = curPos.positions[i];
            if (curPos.obeyRotation)
            {
                desiredRotation = (int)curPos.transform.eulerAngles.y;
            }
        }
    }

    int CalcDirection()
    {
        int iPos = -1;
        int choosePos = desiredRotation % 360;
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

    private void RotRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation += 90;
        }
        else if (curPos.obeyRotation)
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
    }

    private void RotLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!curPos.obeyRotation && !dialog.IsDialogueRunning)
        {
            desiredRotation -= 90;
        }
        else if (curPos.obeyRotation)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && dialog.IsDialogueRunning)
        {
            dialog.OnViewRequestedInterrupt();
        }

        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.LerpAngle(transform.eulerAngles.y, desiredRotation, Time.deltaTime * rotationSpeed), transform.eulerAngles.z);
        gameObject.transform.position = Vector3.Lerp(transform.position, curPos.transform.position, Time.deltaTime * moveSpeed);

        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.transform.name);
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 1f, 1 << 0) && Mouse.current.leftButton.isPressed)
            {
                //Debug.Log("Walk");
                Vector3 moveTarget = navPos.position;
                doll.destination = moveTarget;
                //direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                //lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
            }

            if (hit.transform.gameObject.CompareTag("Object") && Mouse.current.leftButton.wasPressedThisFrame && !dialog.IsDialogueRunning)
            {
                dialog.StartDialogue(hit.transform.gameObject.name);
            }
        }

        

        foreach (GameObject obj in objects)
        {
            if (obj != hit.transform.gameObject || dialog.IsDialogueRunning)
            {
                obj.layer = 0;
            }
            else if (obj == hit.transform.gameObject && hit.transform.gameObject.CompareTag("Object"))
            {
                obj.layer = 8;
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
        //Debug.Log(desiredFOV);
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, desiredFOV, Time.deltaTime * 4);

    }


}
