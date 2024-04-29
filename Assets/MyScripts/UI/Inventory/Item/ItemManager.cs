using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : RPGMonoBehaviour
{
    public InteractionSettings interactionSettings;

    public static ItemManager Instance { get; private set; }

    public List<Item> itemList = new List<Item>();


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
        if (slot.isEmpty) return;

        switch (slot.slotItem.type)
        {
            default: DefaultItemUse(slot); break;
            case ItemType.ToolOrWeapon: EquipItem(slot);break;
            case ItemType.Placeable: PlaceItem(slot); break;
            case ItemType.Consumeable: ConsumeItem(slot); break;
        }
    }
    private void ConsumeItem(ItemSlot slot)
    {
        Debug.Log("You have consumed" + slot.slotItem.itemName);
        slot.Remove(1);
    }
    private void EquipItem(ItemSlot slot)
    {
        Debug.Log("Equipping" + slot.slotItem.itemName);

    }
    private void PlaceItem(ItemSlot slot)
    {
        Debug.Log("Placing" + slot.slotItem.itemName);
    }
    private   void DefaultItemUse(ItemSlot slot)
    {
        Debug.Log($"Using {slot.slotItem.itemName}");
    }
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
