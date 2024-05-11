using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillSlot : ItemSlot
{
    //public PlayerCtrl playerCtrl;
    //public ItemManager itemManager;
    //public ItemContainer inventory;
    [SerializeField] protected Button assignButton;

    public override void Initialize()
    {
        this.iconImage = transform.Find("IconImage").GetComponent<Image>();
        iconImage.gameObject.SetActive(true);
        assignButton.gameObject.SetActive(false);
    }
    //Add Item

    public override bool Add(Item item)
    {
        if (IsAddable(item))
        {
            slotItem = item;
            itemCount++;
            OnSlotModified();
            return true;
        }
        else
            return false;
    }

    //Thiết lập itemCount , UI
    private void OnSlotModified()
    {
        if (!IsEmpty)
        {
            iconImage.sprite = slotItem.icon;
            iconImage.color = Color.white;
            iconImage.gameObject.SetActive(true);
            assignButton.gameObject.SetActive(true);
        }
        else
        {
            itemCount = 0;
            slotItem = null;
            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
        }
    }
    

}
