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

	private float desiredX = 1600;
	public float drawerSpeed;
	private RectTransform trans;
	public AudioManager audios;

    // Start is called before the first frame update
    void Start()
    {
		trans = GetComponent<RectTransform>();
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
		trans.anchoredPosition = new Vector2(Mathf.Lerp(trans.anchoredPosition.x, desiredX, Time.deltaTime * drawerSpeed), trans.anchoredPosition.y);
    }

	public void Rearrange(ItemScriptableObject heldItem, ItemScriptableObject hoverItem)
	{
		int heldItemIndex = inv.IndexOf(heldItem);
		int hoverItemIndex = inv.IndexOf(hoverItem);
		inv[heldItemIndex] = hoverItem;
		inv[hoverItemIndex] = heldItem;
		UpdateInventory();
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

    public void ToggleInventory()
	{
		if (desiredX == 1600)
		{
			desiredX = 800;
			audios.PlayDrawerOpen();
		}
		else
		{
			desiredX = 1600;
			audios.PlayDrawerClosed();
		}
	}
}
