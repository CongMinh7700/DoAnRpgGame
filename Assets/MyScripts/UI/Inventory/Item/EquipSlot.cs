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
        if (!IsEmpty && slotItem != null)
        {
           
           
            switch (slotItem.type)
            {
                case ItemType.Helmet:
                    ItemManager.isEquippedHelmet = false;
               
                    break;
                case ItemType.Armor:
                    ItemManager.isEquippedArmor = false;
                    break;
                case ItemType.Gloves:
                    ItemManager.isEquippedGloves = false;
                    break;
                case ItemType.Weapon:
                    ItemManager.isEquippedWeapon = false;
                    ItemManager.bonusAttack = 0;
                    break;
                default:
                    break;
            }
            inventory.inventoryEvents.AddItem(slotItem);
            Remove(1);
            playerCtrl.DamageReceiver.SetHpMax(playerCtrl.HitableObjectSO.hpMax);
            playerCtrl.DamageReceiver.SetDefense(playerCtrl.HitableObjectSO.defense);

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
