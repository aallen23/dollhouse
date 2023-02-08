using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{
    public List<ItemScriptableObject> inv;
    public GameObject contentParent;
    public GameObject itemDisplayTemplate;

    // Start is called before the first frame update
    void Start()
    {
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        foreach (Transform child in contentParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemScriptableObject i in inv)
        {
            GameObject newItem = Instantiate(itemDisplayTemplate, contentParent.transform);
            newItem.GetComponent<Image>().sprite = i.displaySprite;
            //newItem.GetComponent<Button>().onClick.AddListener()
        }
    }

    
}
