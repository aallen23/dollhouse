using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

//Inventory System Controller
public class InventorySystem : MonoBehaviour
{
    [Tooltip("List of ItemScriptableObjects currently in the inventory.")] public List<ItemScriptableObject> inv;
    [Tooltip("GameObject to Instantiate Inventory Items into.")] public GameObject contentParent;
    [Tooltip("Prefab template for Inventory Items.")] public GameObject itemDisplayTemplate;

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
            newItem.GetComponent<InventoryItem>().item = i;
            //newItem.GetComponent<Button>().onClick.AddListener()
        }
    }

    
}
