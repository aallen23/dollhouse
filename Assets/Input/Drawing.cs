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

    public LineRenderer curLineRenderer;
    public Vector3 mousePos;
    public Transform mouseTransform;

    Vector3 lastPos;

    public Transform drawParent;
    public List<Transform> drawPoints;
    private int numDrawn;
    public CameraPosition DrawPos;

    public GameObject objectToDestroy;
    public ItemScriptableObject itemToAdd;

    private void Start()
    {
        inputMap = FindObjectOfType<P2PCameraController>().inputMap;
        //inputMap.PointToPoint.MousePos.started += MousePos_started;
        inputMap.PointToPoint.MousePos.performed += MousePos_performed;
        inputMap.PointToPoint.Interact.performed += Interact_performed;
        inputMap.PointToPoint.Interact.canceled += Interact_canceled;
        //CreateBrush();
        foreach (Transform point in drawParent)
        {
            drawPoints.Add(point);
        }
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        if (curLineRenderer)
        {
            if (numDrawn < drawPoints.Count)
            {
                foreach (Transform t in drawPoints)
                {
                    t.GetComponent<Image>().color = Color.red;
                }
                numDrawn = 0;
            }
            else
            {
                Debug.Log("Draw Success");
                if (objectToDestroy)
                {
                    FindObjectOfType<P2PCameraController>().Travel(FindObjectOfType<P2PCameraController>().curPos.positions[2]);
                    objectToDestroy.SetActive(false);
                    //Destroy(transform.root.gameObject);
                }
                if (itemToAdd)
                {
                    InventorySystem invSystem = FindObjectOfType<InventorySystem>();
                    invSystem.inv.Add(itemToAdd);
                    invSystem.UpdateInventory();
                }
                
            }
            Destroy(curLineRenderer.gameObject);
            curLineRenderer = null;
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 0)
        {
            return;
        }
        if (FindObjectOfType<P2PCameraController>().curPos == DrawPos)
        {
            CreateBrush();
        }
    }

    private void MousePos_performed(InputAction.CallbackContext obj)
    {
        //Vector2 mousePos = cam.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        //Debug.Log(mousePos);
        if (mousePos != lastPos && curLineRenderer && FindObjectOfType<P2PCameraController>().curPos == DrawPos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    private void MousePos_started(InputAction.CallbackContext obj)
    {
        //CreateBrush();
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush, transform.parent);
        curLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //Vector2 mousePos = cam.ScreenToWorldPoint(inputMap.PointToPoint.MousePos.ReadValue<Vector2>());

        //curLineRenderer.SetPosition(0, mousePos);
        //curLineRenderer.SetPosition(1, mousePos);
        AddAPoint(mousePos);
    }

    void AddAPoint(Vector3 pointPos)
    {
        curLineRenderer.positionCount++;
        int positionIndex = curLineRenderer.positionCount - 1;
        curLineRenderer.SetPosition(positionIndex, pointPos);
        if (drawPoints.Contains(mouseTransform))
        {
            if (mouseTransform.GetComponent<Image>().color != Color.green)
            {
                mouseTransform.GetComponent<Image>().color = Color.green;
                numDrawn++;
            }
        }
    }
}
