using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaPuzzle : MonoBehaviour
{
    public ItemScriptableObject umbBlue;
    public ItemScriptableObject umbRed;
    public ItemScriptableObject umbYellow;
    public ItemScriptableObject umbGreen;

    public ItemScriptableObject stand1, stand2, stand3, stand4;

    private void Start()
    {
        //GetComponent<Collider>().enabled = false;
        //GetComponent<MeshRenderer>().enabled = false;
        gameObject.SetActive(false);
    }

    public void UpdateUmbrellas(string inStand, ItemScriptableObject umbrellaColor)
    {
        switch (inStand)
        {
            case "deck_box_1":
                stand1 = umbrellaColor;
                break;
            case "deck_box_2":
                stand2 = umbrellaColor;
                break;
            case "deck_box_3":
                stand3 = umbrellaColor;
                break;
            case "deck_box_4":
                stand4 = umbrellaColor;
                break;
        }
        Check();
    }

    public void Check()
    {
        //Debug.Log((umbLeft.addedItem == umbYellow) + " " + (umbMidLeft.addedItem == umbRed) + " " + (umbMidRight.addedItem == umbBlue) + " " + (umbRight.addedItem == umbGreen));
        if (stand1 == umbYellow &&
            stand2 == umbRed &&
            stand3 == umbBlue &&
            stand4 == umbGreen)
        {
            //GetComponent<Collider>().enabled = true;
            //GetComponent<MeshRenderer>().enabled = true;
            gameObject.SetActive(true);
			GetComponent<AudioSource>().Play();
        }
    }
}
