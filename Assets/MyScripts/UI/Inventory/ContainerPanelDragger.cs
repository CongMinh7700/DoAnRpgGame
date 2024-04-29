using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//Drag Inventory
public class ContainerPanelDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    //[Header("Drag Inventory")]
    public static event Action OnContainerPanelDrag;
    private bool isDragging = false;
    private Vector2 offset;
    private Transform myContainerPanel;
    private void Awake()
    {
        myContainerPanel = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = new Vector2(myContainerPanel.localPosition.x, myContainerPanel.localPosition.y) - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        myContainerPanel.parent.SetAsLastSibling();
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging == false) return;
        myContainerPanel.localPosition = Input.mousePosition + new Vector3(offset.x, offset.y, 0);
        OnContainerPanelDrag?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myContainerPanel.parent.SetAsLastSibling();
    }
}
