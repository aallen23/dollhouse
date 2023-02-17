using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject cursorSprite;
    public GameObject cursorSpritePrefab;
    private P2PCameraController player;
    public Controls inputMap;
    public ItemScriptableObject item;
    private void Start()
    {
        player = FindObjectOfType<P2PCameraController>();
        gameObject.GetComponent<Image>().sprite = item.displaySprite;
    }

    private void Update()
    {

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        cursorSprite = Instantiate(cursorSpritePrefab, transform.GetComponentInParent<Canvas>().transform);
        cursorSprite.GetComponent<Image>().sprite = item.displaySprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        player.heldItem = item;
        player.cursorSprite = cursorSprite;
        cursorSprite.transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //player.heldItem = null;
        Destroy(cursorSprite);
    }
}
