using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : RPGMonoBehaviour
{
    public InteractionSettings interactionSettings;
    //Check Equipped
    public static bool isEquippedWeapon;
    public static bool isEquippedArmor;
    public static bool isEquippedHelmet;
    public static bool isEquippedGloves;
    public static bool isChange;
    //bonusValue
    public static int hpMaxBonus;
    public static int healBonus;
    public static int bonusAttack;
    public static int bonusDefense;
    public static int manaBonus;
    public static int staminaBonus;
    //WeaponName
    public static string weaponName;

    public PlayerCtrl playerCtrl;
    [SerializeField] protected CharacterStats slotCharacter;
    [SerializeField] protected ItemContainer inventory;

    public List<Item> itemList = new List<Item>();
    public List<GameObject> weapons = new List<GameObject>();

    public static ItemManager Instance { get; private set; }

    private void Update()
    {
        Debug.LogWarning("IsEqquippedWeapon :" + isEquippedWeapon);
        //Debug.LogWarning("isEquippedArmor :" + isEquippedArmor);
        //Debug.LogWarning("isEquippedHelmet :" + isEquippedHelmet);
        //Debug.LogWarning("isEquippedGloves :" + isEquippedGloves);
        Debug.LogWarning("HpBonus : " + hpMaxBonus);
        //Debug.LogWarning("Defense :" + defense);
        Debug.LogWarning("AttackBonus :" + bonusAttack);
        EquipWeapon();
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
            case ItemType.Consumeable: ConsumeItem(slot); break;
            case ItemType.Helmet:
            case ItemType.Armor:
            case ItemType.Gloves:
            case ItemType.Weapon:
                EquipItem(slot);
                break;
        }
    }

    //Nơi xử lý chức năng cho item
    private void ConsumeItem(ItemSlot slot)
    {
        if (slot.slotItem.isFood)
        {
            BonusAttribute heal = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
            BonusAttribute mana = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "mana");
            if(heal != null)
            {
                playerCtrl.DamageReceiver.Health(heal.attributeValue);
            }
            if(mana != null)
            {
                Debug.Log("Use Mana");
            }

            //Hồi mana
        }

        Debug.Log("You have consumed" + slot.slotItem.itemName);
        slot.Remove(1);
    }

    private void EquipItem(ItemSlot slot)
    {
        ItemType type = slot.slotItem.type;
        int slotIndex = -1;
        switch (type)
        {
            case ItemType.Helmet:
                slotIndex = 0;
                isEquippedHelmet = true;
                break;
            case ItemType.Armor:
                slotIndex = 1;
                isEquippedArmor = true;
                break;
            case ItemType.Gloves:
                slotIndex = 2;
                isEquippedGloves = true;
                break;
            case ItemType.Weapon:
                slotIndex = 3;
                isEquippedWeapon = true;

                break;
            default:
                break;
        }
        if (slotIndex == -1)
        {
            return;
        }
        SwarpEquipment(slotIndex);
        slotCharacter.slots[slotIndex].Add(slot.slotItem);
        slot.Remove(1);
        UpdateStats();
        //EquipWeapon

        Debug.Log("Equipping" + slotCharacter.slots[3].slotItem.itemName);
    }
    public void EquipWeapon()
    {
        foreach (GameObject weapon in weapons)
        {


            if (isEquippedWeapon)
            {
                if (weapon.name == slotCharacter.slots[3].slotItem.itemName)
                {
                    weapon.SetActive(true);
                    weaponName = slotCharacter.slots[3].slotItem.itemName;
                }
                else if (weapon.name != slotCharacter.slots[3].slotItem.itemName)
                {

                    weapon.SetActive(false);
                }
            }
            
            else if(!isEquippedWeapon)
            {
                weapon.SetActive(false);
                weaponName = "";
            }


        }
    }
    public void SwarpEquipment(int index)
    {
        inventory.inventoryEvents.AddItem(slotCharacter.slots[index].slotItem);
        slotCharacter.slots[index].Remove(1);
    }
    private void DefaultItemUse(ItemSlot slot)
    {
        Debug.Log($"Using {slot.slotItem.itemName}");
    }
    public void UpdateStats()
    {
        hpMaxBonus = 0;
        bonusAttack = 0;
        bonusDefense = 0;
        foreach (EquipSlot slot in slotCharacter.slots)
        {
            if (slot.slotItem != null)
            {
                if (!slot.slotItem.isFood)
                {
                    foreach (BonusAttribute bonus in slot.slotItem.bonusAttributes)
                    {
                        switch (bonus.attributeName)
                        {
                            case "health":
                                hpMaxBonus += bonus.attributeValue;
                                break;
                            case "attack":
                                bonusAttack += bonus.attributeValue;
                                break;
                            case "defense":
                                bonusDefense += bonus.attributeValue;
                                break;
                            case "mana":
                                manaBonus += bonus.attributeValue;
                                break;
                                // Add cases for other attribute names if necessary
                        }
                    }
                }
            }


        }

    }
    //Xử lý cho Data
    public Item GetItemByIndex(int index)
    {
        return itemList[index];
    }
    public Item GetItemByName(string name)
    {
        foreach (Item item in itemList) if (item.itemName == name) return item;
        return null;
    }
    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < itemList.Count; i++) if (itemList[i] == item) return i;
        return -1;
    }
}
