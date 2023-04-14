using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaModelSwitcher : MonoBehaviour
{
    public List<GameObject> umbrellaModels;

    public void SwitchModel(int item)
    {
        foreach (GameObject umb in umbrellaModels)
        {
            if (umbrellaModels.IndexOf(umb) == item)
            {
                umb.SetActive(true);
            }
            else
            {
                umb.SetActive(false);
            }
        }
    }
}
