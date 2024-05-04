using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : RPGMonoBehaviour
{
    public InteractionSettings interactionSettings;
    public static bool isEquippedWeapon;
    public static bool isEquippedArmor;
    public static bool isEquippedHelmet;
    public static bool isEquippedGloves;
    public PlayerCtrl playerCtrl;

    public static int hpMaxBonus;
    public static int healBonus;
    public static int attackBonus;
    public static int defenseBonus;
    public static int manaBonus;
    public static int staminaBonus;
    public List<Item> itemList = new List<Item>();//có thể không xài
    public List<GameObject> weapons = new List<GameObject>();
    [SerializeField] protected CharacterStats slotCharacter;
    [SerializeField] protected ItemContainer inventory;


    public static ItemManager Instance { get; private set; }

    private void Update()
    {
        //Debug.LogWarning("IsEqquippedWeapon :" + isEquippedWeapon);
        //Debug.LogWarning("isEquippedArmor :" + isEquippedArmor);
        //Debug.LogWarning("isEquippedHelmet :" + isEquippedHelmet);
        //Debug.LogWarning("isEquippedGloves :" + isEquippedGloves);
        Debug.LogWarning("HpBonus : " + hpMaxBonus);
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
            case ItemType.Placeable: PlaceItem(slot); break;
            case ItemType.Consumeable: ConsumeItem(slot); break;
            case ItemType.Helmet:
            case ItemType.Armor:
            case ItemType.Gloves:
            case ItemType.Weapon:
                EquipItem(slot); // Gọi phương thức EquipItem để xử lý việc trang bị
                break;
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
        ItemType type = slot.slotItem.type;
        int slotIndex = -1;
        switch (type)
        {
            case ItemType.Helmet:
                slotIndex = 0; // Vị trí của mũ
                isEquippedHelmet = true;
                break;
            case ItemType.Armor:
                slotIndex = 1; // Vị trí của giáp
                isEquippedArmor = true;
                break;
            case ItemType.Gloves:
                slotIndex = 2; // Vị trí của găng tay
                isEquippedGloves = true;
                break;
            case ItemType.Weapon:
                slotIndex = 3; // Vị trí của vũ khí
                isEquippedWeapon = true;
                break;
            default:
                break;
        }
        if (slotIndex == -1)
        {
            return;
        }
        slotCharacter.slots[slotIndex].Add(slot.slotItem);
        UpdateStats();
        Debug.Log("Equipping" + slot.slotItem.itemName);
        foreach (GameObject weapon in weapons)
        {
            // Kiểm tra xem vũ khí này có trùng với mục trang bị hay không
            if (weapon.name == slot.slotItem.itemName)
            {
                // Nếu trùng, kích hoạt GameObject của vũ khí đó
                weapon.SetActive(true);
            }
            else
            {
                // Nếu không trùng, vô hiệu hóa GameObject của vũ khí
                weapon.SetActive(false);
            }
        }
        slot.Remove(1);
    }
    
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
        int attack = playerCtrl.HitableObjectSO.damage;
        int defense = playerCtrl.DamageReceiver.Defense; 
        int mana = playerCtrl.playerSO.mana;
        foreach (EquipSlot slot in slotCharacter.slots)
        {
           
            if (slot.slotItem != null)
            {
                if (!slot.slotItem.isFood)
                {
                    BonusAttribute hpBonus = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
                    BonusAttribute attackBonus = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "attack");
                    BonusAttribute defenseBonus = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "defense");
                    BonusAttribute manaBonus = slot.slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "mana");

                    if (hpBonus != null  )
                    {
                      
                        hpMax += hpBonus.attributeValue;
                    }
                    if (attackBonus != null)
                    {

                        attack += attackBonus.attributeValue;
                    }
                    if (defenseBonus != null)
                    {

                        defense += defenseBonus.attributeValue;
                    }
                    if (manaBonus != null)
                    {

                        mana += manaBonus.attributeValue;
                    }

                    hpMaxBonus = hpBonus.attributeValue;
                }
            }
            
            playerCtrl.DamageReceiver.SetHpMax(hpMax);
            Debug.LogWarning("Defense :" + defense);
            playerCtrl.DamageReceiver.SetDefense(defense);
     

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
