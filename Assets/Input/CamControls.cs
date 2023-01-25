using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamControls : MonoBehaviour
{
    //references: https://onewheelstudio.com/blog/2022/1/14/strategy-game-camera-unitys-new-input-system

    private Controls controls;
    private Transform cameraTransform;
    private float zoomHeight;

    [SerializeField]
    private float stepSize = 2.0f,
        zoomDampening = 7.5f,
        zoomSpeed = 2.0f,
        minHeight = 2.0f,
        maxHeight = 25.0f,
        maxRotationSpeed = 0.2f;



    private void Awake()
    {
        controls = new Controls();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    private void ZoomCamera(InputAction.CallbackContext input)
    {
        float inputValue = -input.ReadValue<Vector2>().y / 100;

        if (Mathf.Abs(inputValue) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + inputValue * stepSize;

            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            else if(zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(transform);
    }

    private void RotateCamera(InputAction.CallbackContext input)
    {
        if (!Mouse.current.leftButton.isPressed)
        {
            return;
        }

        float inputX = input.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, inputX * maxRotationSpeed + cameraTransform.rotation.eulerAngles.y, 0f);
    }

    private void OnEnable()
    {
        cameraTransform.LookAt(transform);
        
        zoomHeight = cameraTransform.localPosition.y;

        controls.Camera.Rotate.performed += RotateCamera;
        controls.Camera.Zoom.performed += ZoomCamera;
        controls.Camera.Enable();
    }


    private void OnDisable()
    {
        controls.Camera.Rotate.performed -= RotateCamera;
        controls.Camera.Zoom.performed -= ZoomCamera;
        controls.Camera.Disable();
    }

}
