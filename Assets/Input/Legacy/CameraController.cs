using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;

//Legacy FPS Camera Controller
public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    private Transform playerBody;
    //public PlayerController pc;
    private Camera cam;
    private float xRotation = 0f;
    private RaycastHit hit;
    public NavMeshAgent doll;

    public Controls controls;
    private float desiredFOV;

    //public Slider sensSlider;

    private void Awake()
    {
        controls = new Controls();
        controls.Child.Enable();
        cam = gameObject.GetComponent<Camera>();
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*void Start()
    {
        if (PlayerPrefs.HasKey("mouse"))
        {
            mouseSensitivity = PlayerPrefs.GetFloat("mouse", mouseSensitivity);

        }
        else
        {
            mouseSensitivity = 100f;
        }
        sensSlider.value = mouseSensitivity;
    }*/

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = controls.Child.FPCamera.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(controls.Child.Ray.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out hit))
        {
            //pc
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, 0.25f, 1 << 0) && Mouse.current.leftButton.isPressed)
            {
                //Debug.Log("Walk");
                Vector3 moveTarget = navPos.position;
                doll.destination = moveTarget;
                //direction = (new Vector3(0, moveTarget.y, moveTarget.z) - transform.position).normalized;
                //lookRotation = Quaternion.LookRotation(direction);
                //needToRotate = true;
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
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredFOV, Time.deltaTime * 4);

        mouse *= mouseSensitivity * Time.deltaTime;


        xRotation -= mouse.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouse.x);

    }

    /*public void SetMouseSensitivity()
    {
        mouseSensitivity = sensSlider.value;
        PlayerPrefs.SetFloat("mouse", mouseSensitivity);
    }*/
}
