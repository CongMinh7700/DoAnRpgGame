using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlotUIEvents : RPGMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static event Action OnSlotDrag;
    private static ItemSlot hoveredSlot;
    private ItemSlot mySlot;
    private Image uiSlot;
    private Vector3 dragOffset;
    private Vector3 origin;
    private Color regularColor;
    private Color dragColor;
    private int originalSiblingIndex;


    public bool isBeginDragged { get; private set; } = false;
    protected override void LoadComponents()
    {
        LoadItemSlot();
        LoadSlotUI();
    }
    public virtual void LoadItemSlot()
    {
        if (uiSlot != null) return;
        uiSlot = GetComponent<Image>();
    }
    public virtual void LoadSlotUI()
    {
        if (mySlot != null) return;
        mySlot = GetComponent<ItemSlot>();
    }
    protected override  void Awake()
    {
        originalSiblingIndex = transform.GetSiblingIndex();

        origin = transform.localPosition;
        regularColor = uiSlot.color;
        dragColor = new Color(regularColor.r, regularColor.g, regularColor.b, 0.3f);
    }
    protected void Update()
    {
        if(isBeginDragged && hoveredSlot != null)
        {
           // if(Input.GetKeyDown(Intera))
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
