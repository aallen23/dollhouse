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

    [SerializeField] private float openDrawerX = 1600;
    [SerializeField] private float closedDrawerX = 800;
    [SerializeField] private float openSpeed = 8;
    [SerializeField] private float closeSpeed = 8;
    [SerializeField] private LeanTweenType openEaseType;
    [SerializeField] private LeanTweenType closeEaseType;

	private RectTransform trans;
	public AudioManager audios;

    private bool drawerOpen = false;
    private bool drawerAnimationActive = false;

    private Controls playerControls;

    private void Awake()
    {
        playerControls = new Controls();
        playerControls.Menu.ToggleInventory.started += _ => ToggleInventory();
    }

    // Start is called before the first frame update
    void Start()
    {
		trans = GetComponent<RectTransform>();
        UpdateInventory();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
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
        if(GameManager.Instance.IsGameplayActive())
            OpenDrawer(!drawerOpen);
    }

    private void OpenDrawer(bool isOpen)
    {
        if (drawerAnimationActive)
            return;

        drawerAnimationActive = true;
        drawerOpen = isOpen;

        if(drawerOpen)
            audios.PlayDrawerOpen();
        else
            audios.PlayDrawerClosed();

        LeanTween.moveX(trans, isOpen ? openDrawerX : closedDrawerX, isOpen ? openSpeed : closeSpeed).setEase(isOpen ? openEaseType : closeEaseType).setOnComplete(() => drawerAnimationActive = false); ;
    }
}
