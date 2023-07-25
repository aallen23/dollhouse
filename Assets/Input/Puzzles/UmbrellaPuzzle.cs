using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaPuzzle : MonoBehaviour
{
    [Tooltip("Umbrella Scriptable Objects of each color.")] public ItemScriptableObject umbBlue, umbRed, umbYellow, umbGreen;
	[Tooltip("Umbrella Scriptable Objects that are in each stand.")] public ItemScriptableObject stand1, stand2, stand3, stand4;

	[Tooltip("Puzzle Complete music.")] public AudioSource audioComplete;



    private void Start()
    {
        //GetComponent<Collider>().enabled = false;
        //GetComponent<MeshRenderer>().enabled = false;
        gameObject.SetActive(false);
    }

	//Updates the current value of each stand
    public void UpdateUmbrellas(string inStand, ItemScriptableObject umbrellaColor)
    {
        switch (inStand)
        {
            case "Stand 1":
                stand1 = umbrellaColor;
                break;
            case "Stand 2":
                stand2 = umbrellaColor;
                break;
            case "Stand 3":
                stand3 = umbrellaColor;
                break;
            case "Stand 4":
                stand4 = umbrellaColor;
                break;
        }
        Check();
    }

    public void Check()
    {
		//Current puzzle solution, hardcoded (for now)
        if (stand1 == umbYellow &&
            stand2 == umbRed &&
            stand3 == umbBlue &&
            stand4 == umbGreen)
        {
            gameObject.SetActive(true);
			audioComplete.Play();
        }
    }
}
