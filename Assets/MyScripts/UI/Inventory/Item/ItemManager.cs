﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : RPGMonoBehaviour
{
    public InteractionSettings interactionSettings;
    public static bool isEquipped;


    public List<Item> itemList = new List<Item>();
    public List<GameObject> weapons = new List<GameObject>();


    public static ItemManager Instance { get; private set; }

    protected override  void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }
    //UseItem
    public void UseItem(ItemSlot slot)
    {
        if (slot.IsEmpty) return;

        switch (slot.slotItem.type)
        {
            default: DefaultItemUse(slot); break;
            case ItemType.ToolOrWeapon:
                
                if (!isEquipped) EquipItem(slot);//Có thể lỗi nếu đặt nhầm chỗ
                else UnEquip(slot);
                break;
            case ItemType.Placeable: PlaceItem(slot); break;
            case ItemType.Consumeable: ConsumeItem(slot); break;
        }
    }
  
    //Nơi xử lý chức năng cho item
    private void ConsumeItem(ItemSlot slot)
    {
        Debug.Log("You have consumed" + slot.slotItem.itemName);
        slot.Remove(1);
    }
    private void EquipItem(ItemSlot slot)
    {
        Debug.Log("Equipping" + slot.slotItem.itemName);
        isEquipped = true;
        weapons[0].SetActive(true);
    }
    private void UnEquip(ItemSlot slot)
    {
        Debug.Log("UnEquip" + slot.slotItem.itemName);
        isEquipped = false;
        weapons[0].SetActive(false);
    }
    private void PlaceItem(ItemSlot slot)
    {
        Debug.Log("Placing" + slot.slotItem.itemName);
    }
    private   void DefaultItemUse(ItemSlot slot)
    {
        Debug.Log($"Using {slot.slotItem.itemName}");
    }
    //Chua xai toi
    public Item GetItemByIndex(int index)
    {
        return itemList[index];
    }
    public Item GetItemByName(string name)
    {
        foreach(Item item in itemList)
        {
            if (item.itemName == name) return item;
         
        }
        return null;
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < itemList.Count; i++) if (itemList[i] == item) return i;
        return -1;
    }
}
