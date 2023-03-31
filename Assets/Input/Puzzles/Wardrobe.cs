using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject Chair;

    // Start is called before the first frame update
    void Awake()
    {
        Chair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
