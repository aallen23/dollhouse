using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Controls inputMapping;
    
    private Camera cam;
    private NavMeshAgent agent;
    private float rotateSpeed = 5f;
    private bool needToRotate = false;

    private Vector3 moveTarget = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Quaternion lookRotation = Quaternion.identity;

    private void Awake()
    {
        inputMapping = new Controls();
        inputMapping.Camera.Walk.performed += Walk_performed;

        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (needToRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
             lookRotation, Time.deltaTime * rotateSpeed);
            if (Vector3.Dot(direction, transform.forward) >= 0.97)
            {
                needToRotate = false;
            }
        }
    }

    private void Walk_performed(InputAction.CallbackContext obj)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, .25f, 1 << 0))
            {
                moveTarget = navPos.position;
                direction = (new Vector3(moveTarget.x, 0, moveTarget.z) - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(direction);
                needToRotate = true;
            }
        }
    }
}
