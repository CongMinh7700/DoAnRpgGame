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
    public bool isEmpty { get { return itemCount <= 0; } }
    private Image iconImage;
    private TextMeshProUGUI countText;

    protected override void LoadComponents()
    {
        this.LoadIconImage();
        this.LoadCountText();
    }
    public virtual void LoadIconImage()
    {
        if (this.iconImage != null) return;
        this.iconImage = GetComponentInChildren<Image>();
        Debug.LogWarning(transform.name + "|LoadIconImage|", gameObject);
    }
    public virtual void LoadCountText()
    {
        if (this.countText != null) return;
        this.countText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + "|LoadCountText|", gameObject);
    }

    public void Initialize()
    {
        iconImage.gameObject.SetActive(true);
        countText.text = string.Empty;
    }
    //Add Item
    public bool Add(Item item)
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
    public void RemoveAndDrop(int amount,Vector3 dropPosition)
    {
        for(int i =0;i< amount; i++)
        {
            if(itemCount > 0)
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
        OnSlotModified() ;
    }
    public void ClearAndDrop(Vector3 dropPosition)
    {
        RemoveAndDrop(itemCount, dropPosition);
    }
    //Kiểm tra slot chứa vật phẩm đó có thể thêm vào nữa được không
    private bool IsAddable(Item item)
    {
        if(item != null)
        {
            if (isEmpty) return true;
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
        if (!isEmpty)
        {
            iconImage.sprite = slotItem.icon;
            countText.text = itemCount.ToString();
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            itemCount = 0;
            slotItem = null;
            iconImage.sprite = null;
            countText.text = string.Empty;
            iconImage.gameObject.SetActive(false);
        }
    }
    //Thiết lập data, UI cho slot
    public void SetData(Item item ,int count)
    {
        slotItem = item;
        itemCount = count;
        OnSlotModified();
    }

}
