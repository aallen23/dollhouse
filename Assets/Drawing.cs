using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour
{
    public Controls inputMap;
    public Camera cam;
    public GameObject brush;

    LineRenderer curLineRenderer;

    Vector2 lastPos;

    private void Start()
    {
        inputMap = FindObjectOfType<P2PCameraController>().inputMap;
        inputMap.PointToPoint.MousePos.started += MousePos_started;
        inputMap.PointToPoint.MousePos.performed += MousePos_performed;
    }

    private void MousePos_performed(InputAction.CallbackContext obj)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        if (mousePos != lastPos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    private void MousePos_started(InputAction.CallbackContext obj)
    {
        CreateBrush();
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        curLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = cam.ScreenToWorldPoint(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());

        curLineRenderer.SetPosition(0, mousePos);
        curLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        curLineRenderer.positionCount++;
        int positionIndex = curLineRenderer.positionCount - 1;
        curLineRenderer.SetPosition(positionIndex, pointPos);
    }
}
