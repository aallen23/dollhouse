using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightconestuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 pt = collision.GetContact(0).point;
        TryGetComponent(out Mesh mesh);
        //mesh.getver
    }
}
