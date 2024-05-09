using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuickItemSlot : MonoBehaviour
{
    public Item slotItem;
    public int itemCount;
    public bool IsEmpty { get { return itemCount <= 0; } }
    [SerializeField] public KeyCode key;
    [Header("UI Elements")]
    public Image iconImage;
    public TextMeshProUGUI countText;

    // Method to initialize the QuickItemSlot
    public virtual void Initialize()
    {
        this.iconImage = transform.Find("IconImage").GetComponent<Image>();
        this.countText = GetComponentInChildren<TextMeshProUGUI>();
        iconImage.gameObject.SetActive(true);
        countText.text = string.Empty;
    }
    //Add Item
    public virtual bool Add(Item item)
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
    private void Update()
    {
        UseItem();
    }
    //Remove and Drop
    public  virtual void UseItem()
    {
        if (Input.GetKeyDown(key))
        {
            ConsumeItem();
            Debug.Log("KeyCode "+ key.ToString());
        }
    }
    private void ConsumeItem()
    {
        if (slotItem == null) return;
        if (slotItem.isFood)
        {
            BonusAttribute heal = slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
            BonusAttribute mana = slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "mana");
            if (heal != null)
            {
                // playerCtrl.DamageReceiver.Health(heal.attributeValue);
                Debug.Log("Heal");
            }
            if (mana != null)
            {
                Debug.Log("Use Mana");
            }

            //Hồi mana
        }
        Debug.Log("You have consumed" + slotItem.itemName);
        Remove(1);
    }
   

    public void Remove(int amount)
    {
        itemCount -= amount > itemCount ? itemCount : amount;
        OnSlotModified();
    }
    public void Clear()
    {
        itemCount = 0;
        OnSlotModified();
    }
   
    //Kiểm tra slot chứa vật phẩm đó có thể thêm vào nữa được không
    public bool IsAddable(Item item)
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
            countText.text = itemCount.ToString();
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            itemCount = 0;
            slotItem = null;
            iconImage.sprite = null;
            if (countText != null)
            {
                countText.text = string.Empty;
            }

            iconImage.gameObject.SetActive(false);
        }
    }
    //Thiết lập data, UI cho slot
    public void SetData(Item item, int count)
    {
        slotItem = item;
        itemCount = count;
        OnSlotModified();
    }

  

}

