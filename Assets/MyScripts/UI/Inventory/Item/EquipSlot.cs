using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSlot : ItemSlot
{
    public PlayerCtrl playerCtrl;
    public ItemManager itemManager;
    public ItemContainer inventory;
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
    public virtual void UnEquip()
    {
        if (!IsEmpty)
        {
            inventory.inventoryEvents.AddItem(slotItem);
            Remove(1);
            ItemManager.isEquipped = false;
            playerCtrl.DamageReceiver.SetHpMax(playerCtrl.HitableObjectSO.hpMax);
        }
       

    }
    public void UpdateStats()
    {
        int hpMax = 0 ;
        if(slotItem != null && !slotItem.isFood)
        {
            foreach (BonusAttribute bonus in slotItem.bonusAttributes)
            {
                if (bonus.attributeName == "health")
                {
                    hpMax = playerCtrl.DamageReceiver.HPMax + bonus.attributeValue;
                    playerCtrl.DamageReceiver.SetHpMax(hpMax);
                }
               
            }
        }
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
