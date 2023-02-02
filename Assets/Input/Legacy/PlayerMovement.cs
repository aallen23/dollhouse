using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Controls inputMapping;
    
    private Camera cam;
    public Transform camParent;
    private NavMeshAgent agent;
    //private float rotateSpeed = 5f;
    //[SerializeField]private bool needToRotate = false;

    private Vector3 moveTarget = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Quaternion lookRotation = Quaternion.identity;
    private Vector2 inputVector;

    private void Awake()
    {
        inputMapping = new Controls();
        inputMapping.Camera.Walk.performed += Walk_performed;
        inputMapping.Camera.Pan.performed += Pan_performed;

        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10f;
    }

    private void Pan_performed(InputAction.CallbackContext obj)
    {
        
    }

    private void OnEnable() => inputMapping.Enable();
    private void OnDisable() => inputMapping.Disable();

    private void Update()
    {
        inputVector = inputMapping.Camera.Pan.ReadValue<Vector2>();
        //camParent.localPosition += new Vector3(inputVector.x, inputVector.y, 0) * Time.deltaTime * 20;
        var forward = camParent.forward;
        var right = camParent.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * inputVector.y + right * inputVector.x;

        //now we can apply the movement:
        camParent.position += desiredMoveDirection * 20 * Time.deltaTime;
        /*if (needToRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
             lookRotation, Time.deltaTime * rotateSpeed);
            if (Vector3.Dot(direction, transform.forward) >= 0.9)
            {
                needToRotate = false;
                agent.isStopped = false;
                agent.destination = moveTarget;
            }
        }*/
    }

    private void Walk_performed(InputAction.CallbackContext obj)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 300f))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 0.25f, 1 << 0))
            {
                Debug.Log("Walk");
                moveTarget = navPos.position;
                agent.destination = moveTarget;
                direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
            }
        }
    }
}
