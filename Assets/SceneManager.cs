using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneManager : MonoBehaviour
{
    public NavMeshSurface surface;
    //public Transform rotateObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotateObject.localRotation = Quaternion.Euler(new Vector3(0, 15 * Time.deltaTime, 0) + rotateObject.localEulerAngles);
        surface.BuildNavMesh();
    }
}
