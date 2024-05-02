using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSlot : ItemSlot
{
    [Header("ExternalSLot")]

    private Image iconImage;
    public PlayerDamageReceiver playerDamageReceiver;

    public override void Initialize()
    {
        this.iconImage = transform.Find("IconImage").GetComponent<Image>();
        iconImage.gameObject.SetActive(true);
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
    //Remove and Drop
    public virtual void UpdateStats()
    {
        if (slotItem != null && slotItem.type == ItemType.ToolOrWeapon)
        {
            int hpMax = playerDamageReceiver.HPMax + slotItem.effectValue;
            playerDamageReceiver.SetHpMax(hpMax);
        }

    }


    //Kiểm tra slot chứa vật phẩm đó có thể thêm vào nữa được không
    private bool IsAddable(Item item)
    {
        if (item != null)
        {
            if (IsEmpty) return true;
            else
            {
                if (item == slotItem && itemCount < item.itemPerSlot) return true;
                else return false;
            }
        }
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
