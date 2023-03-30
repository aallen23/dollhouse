using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaMaking : MonoBehaviour
{
    public GameObject SugarCube;
    public bool addedSugar;

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
        if (collision.gameObject == SugarCube)
        {
            collision.gameObject.SetActive(false);
            addedSugar = true;
        }
    }
}
