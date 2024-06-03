using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : RPGMonoBehaviour
{
    [Header("Item Slot")]
    public Item slotItem;
    public int itemCount;
    public bool IsEmpty { get { return itemCount <= 0; } }
    [HideInInspector] public Image iconImage;
    private TextMeshProUGUI countText;
    public virtual void Initialize()
    {
        this.iconImage = transform.Find("IconImage").GetComponent<Image>();
        this.countText = GetComponentInChildren<TextMeshProUGUI>();
        iconImage.gameObject.SetActive(true);
        if (this.countText == null) return;
        countText.text = string.Empty;
    }
    private void Update()
    {
   
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
    //Remove and Drop
    public void RemoveAndDrop(int amount, Vector3 dropPosition)
    {
        for (int i = 0; i < amount; i++)
        {
            if (itemCount > 0)
            {
                //tha item ra ngoai cách olayer theo offset
                itemCount--;
            }
        }
        OnSlotModified();
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
    public void ClearAndDrop(Vector3 dropPosition)
    {
        RemoveAndDrop(itemCount, dropPosition);
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
            iconImage.gameObject.SetActive(true);
            if (this.countText == null) return;
            countText.text = itemCount.ToString();
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

    public  void AssignItem()
    {

            QuickBar quickBar = FindObjectOfType<QuickBar>();
            foreach (QuickItemSlot quickItemSlot in quickBar.itemSlots)
            {
                if (Input.GetKey(quickItemSlot.key))
                {
                if (!IsEmpty && slotItem.isFood)
                {
                    if (quickItemSlot != null)
                    {
                        
                        quickItemSlot.BackToInventory();
                        quickItemSlot.SetData(slotItem, itemCount);
               
                        Clear();
                        break;
                    }
                   
                }

       
            }
        }

    }


    public void AssignSkill()
    {
        QuickBar quickBar = FindObjectOfType<QuickBar>();
        bool skillAssigned = false;
        foreach (QuickSkillSlot quickSkillSlot in quickBar.skillSlots)
        {
            if (quickSkillSlot.slotItem != null && quickSkillSlot.slotItem.itemName == slotItem.itemName)
            {
                skillAssigned = true;
                break;
            }
        }
        if (skillAssigned)
        {
            Debug.Log("Skill is assigned");
            return;
        }
        foreach (QuickSkillSlot quickSkillSlot in quickBar.skillSlots)
        {
            if (Input.GetKey(quickSkillSlot.key))
            {
                if (!IsEmpty && slotItem.type == ItemType.Skill)
                {
                    if (quickSkillSlot != null)
                    {
                        SFXManager.Instance.PlaySFXClick();
                        quickSkillSlot.SetData(slotItem, itemCount);
                        quickSkillSlot.currentCooldown = 0f;
                        break;
                    }
                }
            }
        }
    }

}
