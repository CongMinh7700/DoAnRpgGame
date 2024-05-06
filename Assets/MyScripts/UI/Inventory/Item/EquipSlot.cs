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
                    break;
                default:
                    break;
            }
            this.ReculateBonusValue();     
            inventory.inventoryEvents.AddItem(slotItem);
            Remove(1);
        }
    }
    public void ReculateBonusValue()
    {
        int removedBonusAttack = 0;
        int removedBonusHealth = 0;
        int removedBonusDefense = 0;
        foreach (BonusAttribute bonus in slotItem.bonusAttributes)
        {
            switch (bonus.attributeName)
            {
                case "health":
                    removedBonusHealth = bonus.attributeValue;
                    break;
                case "defense":
                    removedBonusDefense = bonus.attributeValue;
                    break;
                case "attack":
                    removedBonusAttack = bonus.attributeValue;
                    break;
                    // Thêm các trường hợp xử lý cho các loại bonus khác nếu cần thiết
            }
        }
        ItemManager.bonusAttack -= removedBonusAttack;
        ItemManager.hpMaxBonus -= removedBonusHealth;
        ItemManager.bonusDefense -= removedBonusDefense;
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
