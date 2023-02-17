using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionTracker : MonoBehaviour
{
    [SerializeField]
    private int fearfactor;
    private int cookieCount;

    [SerializeField]
    private Slider fearSlider;

    [SerializeField]
    private GameObject cookiesUI;

    // Start is called before the first frame update
    void Start()
    {
        fearfactor = 0;
        cookieCount = 4;
    }

    public void ChangeFearLvl(int i)
    {
        if(fearfactor >= 0)
        {
            fearfactor += i;
            if(fearfactor < 0)
            {
                fearfactor = 0;
            }
            UpdateUI(fearfactor);
        }
    }

    public void UpdateUI(int lvl)
    {
        fearSlider.value = fearfactor;

        if(lvl < 4)
        {
            //trigger neutral
        }
        if(lvl < 8 && lvl > 3)
        {
            //trigger scared
        }
        if(lvl > 7)
        {
            //trigger terrified
        }
    }

    public void AddCookie()
    {
        cookieCount += 1;
        cookiesUI.transform.GetChild(cookieCount - 1).gameObject.GetComponent<Image>().enabled = true;
        cookiesUI.transform.GetChild(cookieCount - 1).gameObject.GetComponent<Button>().enabled = true;
    }

    public void EatCookie()
    {
        if (cookieCount > 0)
        {
            cookiesUI.transform.GetChild(cookieCount - 1).gameObject.GetComponent<Image>().enabled = false;
            cookiesUI.transform.GetChild(cookieCount - 1).gameObject.GetComponent<Button>().enabled = false;
            cookieCount -= 1;
            ChangeFearLvl(-1);
        }
    }
}
