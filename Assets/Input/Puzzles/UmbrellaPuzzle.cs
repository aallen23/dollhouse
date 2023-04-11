using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaPuzzle : MonoBehaviour
{
    public ObjectData umbLeft;
    public ObjectData umbMidLeft;
    public ObjectData umbMidRight;
    public ObjectData umbRight;

    public ItemScriptableObject umbBlue;
    public ItemScriptableObject umbRed;
    public ItemScriptableObject umbYellow;
    public ItemScriptableObject umbGreen;

    public GameObject objBlue, objRed, objYellow, objGreen;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void SwapUmbrella(ItemScriptableObject umb, ObjectData stand)
    {

    }

    public void Check()
    {
        //Debug.Log((umbLeft.addedItem == umbYellow) + " " + (umbMidLeft.addedItem == umbRed) + " " + (umbMidRight.addedItem == umbBlue) + " " + (umbRight.addedItem == umbGreen));
        if (umbLeft.addedItem == umbYellow && 
            umbMidLeft.addedItem == umbRed && 
            umbMidRight.addedItem == umbBlue && 
            umbRight.addedItem == umbGreen)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
