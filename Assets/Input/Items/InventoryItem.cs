using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool dragging;
    private GameObject cursorSprite;
    public GameObject cursorSpritePrefab;
    public Controls inputMap;

    private void Start()
    {
        inputMap = new Controls();
        inputMap.PointToPoint.Enable();
    }
    private void Update()
    {
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        cursorSprite = Instantiate(cursorSpritePrefab, transform.GetComponentInParent<Canvas>().transform);
        cursorSprite.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;
        cursorSprite.transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        Destroy(cursorSprite);
    }
}
