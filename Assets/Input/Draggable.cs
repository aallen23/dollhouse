using UnityEngine.InputSystem;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Tooltip("The Input Map we are using.")] private Controls inputMap;
    private Camera cam;
    Vector3 worldPoint;

    // Start is called before the first frame update
    void Start()
    {
        inputMap = FindObjectOfType<P2PCameraController>().inputMap;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = inputMap.PointToPoint.MousePos.ReadValue<Vector2>();
        worldPoint = cam.ScreenToWorldPoint(mousePos);
        Debug.Log(worldPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(worldPoint, 20f);
    }
}
