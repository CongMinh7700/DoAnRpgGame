using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : RPGMonoBehaviour
{
    public InteractionSettings interactionSettings;
    public static bool isEquipped;
    public PlayerCtrl playerCtrl;

    public List<Item> itemList = new List<Item>();//có thể không xài
    public List<GameObject> weapons = new List<GameObject>();
    [SerializeField] protected CharacterStats slotCharacter;


    public static ItemManager Instance { get; private set; }

    private void Update()
    {
        Debug.Log("IsEqquipped :" + isEquipped);
        
    }
    protected override void Awake()
    {
        #region Singleton
        if (Instance == null)
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
                EquipItem(slot);//Có thể lỗi nếu đặt nhầm chỗ
                //if (!isEquipped) 
                //else UnEquip(slot);
                break;
            case ItemType.Placeable: PlaceItem(slot); break;
            case ItemType.Consumeable: ConsumeItem(slot); break;
        }
    }

    //Nơi xử lý chức năng cho item
    private void ConsumeItem(ItemSlot slot)
    {
        if (slot.slotItem.isFood)
        {
            BonusAttribute heal = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
            playerCtrl.DamageReceiver.Health(heal.attributeValue);
        }

        Debug.Log("You have consumed" + slot.slotItem.itemName);
        slot.Remove(1);
    }
    private void EquipItem(ItemSlot slot)
    {
        slotCharacter.slots[1].Add(slot.slotItem);
        UpdateStats();
        Debug.Log("Equipping" + slot.slotItem.itemName);
        isEquipped = true;
        weapons[0].SetActive(true);
        slot.Remove(1);
    }
    //public void UnEquip(ItemSlot slot)
    //{
    //    Debug.Log("UnEquip" + slot.slotItem.itemName);
    //    isEquipped = false;
    //    slot.Add(slot.slotItem);
    //    weapons[0].SetActive(false);
    //    slotCharacter.slots[1].UnEquip();

    //}
    private void PlaceItem(ItemSlot slot)
    {
        Debug.Log("Placing" + slot.slotItem.itemName);
    }
    private void DefaultItemUse(ItemSlot slot)
    {
        Debug.Log($"Using {slot.slotItem.itemName}");
    }

    public void UpdateStats()
    {
        int hpMax = playerCtrl.DamageReceiver.HPMax;
        foreach (EquipSlot slot in slotCharacter.slots)
        {
           
            if (slot.slotItem != null)
            {
                if (!slot.slotItem.isFood)
                {
                    BonusAttribute healthBonus = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
                    
                    if (healthBonus != null  )
                    {
                      
                        hpMax += healthBonus.attributeValue;
                    }
                    
                }
            }
            
            playerCtrl.DamageReceiver.SetHpMax(hpMax);


        }
    }
    //Chua xai toi
    public Item GetItemByIndex(int index)
    {
        return itemList[index];
    }
    public Item GetItemByName(string name)
    {
        foreach (Item item in itemList)
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
