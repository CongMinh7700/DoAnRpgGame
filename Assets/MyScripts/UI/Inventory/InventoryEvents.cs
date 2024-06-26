using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryEvents : RPGMonoBehaviour
{
   [SerializeField] protected ItemContainer itemContainer;
    protected override void LoadComponents()
    {
        LoadItemContainer();
    }
    public virtual void LoadItemContainer()
    {
        if (this.itemContainer != null) return;
        this.itemContainer = GetComponent<ItemContainer>();
    }
    public void ToggleUI()
    {
        CloseSlotOptionMenu();
        if (itemContainer.mainContainerUI.gameObject.activeSelf && itemContainer.isContainerUIOpen)
        {
            itemContainer.isContainerUIOpen = false;
            Time.timeScale = 1;
            StartCoroutine(Utils.TweenScaleOut(itemContainer.mainContainerUI.gameObject, 50, false));
        }
        else if (!itemContainer.mainContainerUI.gameObject.activeSelf && !itemContainer.isContainerUIOpen)
        {
            itemContainer.isContainerUIOpen = true;
            Time.timeScale = 0;
            StartCoroutine(Utils.TweenScaleIn(itemContainer.mainContainerUI.gameObject, 50, Vector3.one));
        }
    }
    //RemoveItem
    public void OnRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        SFXManager.Instance.PlaySFXClick();
        if (itemContainer.dropItemGameObjects) slot.RemoveAndDrop(1, itemContainer.containerInteractor.ItemDropPosition);//không sử dụng drop
        else slot.Remove(1);
    }
    //mới Thêm
    public void OnTransferToInventoryClicked(ItemSlot slot, Interactor interactor)//Không sử dụng chuyển(dành cho rương đồ)
    {
        SFXManager.Instance.PlaySFXClick();
        Utils.TransferItemQuantity(slot, interactor.inventory, slot.itemCount);
    }
    //Mở slotOption
    public void OpenSlotOptionMenu()
    {
        itemContainer.slotOptionMenu.SetActive(false);
        itemContainer.slotOptionMenu.transform.position = Input.mousePosition;
        StartCoroutine(Utils.TweenScaleIn(itemContainer.slotOptionMenu, 50, Vector3.one));//chứa set tf.active

    }
    //Đóng slotOption
    public void CloseSlotOptionMenu()
    {
        SFXManager.Instance.PlaySFXClick();
        itemContainer.slotOptionMenu.SetActive(false);
        itemContainer.itemInfoPanel.SetActive(false);
    }

    //DropItem
    public void OnBulkRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        SFXManager.Instance.PlaySFXClick();
        if (itemContainer.dropItemGameObjects) slot.RemoveAndDrop(slot.itemCount, interactor.ItemDropPosition);
        else slot.Remove(slot.itemCount);
    }
    //Sử dụng item
    public void OnUseItemClicked(ItemSlot slot, Interactor interactor)
    {
        ItemManager.Instance.UseItem(slot);
    }
    //Xem info
    public void OnItemInfoClicked(ItemSlot slot, Interactor interactor)
    {
        SFXManager.Instance.PlaySFXClick();
        itemContainer.itemInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = slot.slotItem.itemInformation;
        itemContainer.itemInfoPanel.SetActive(!itemContainer.itemInfoPanel.activeSelf);
    }
    //slot = null thì tắt option
    public void OnSlotButtonEventFinished(ItemSlot slot)
    {
        if (slot.IsEmpty)
        {
            CloseSlotOptionMenu();
        }
    }
    //key ở đây là I
    public void CheckForUIToggleInput()
    {
        if (Input.GetKeyDown(itemContainer.UIToggleKey)) ToggleUI();
        //Debug.Log("UIToggle");
    }

    public bool AddItem(Item item)
    {

        for (int i = 0; i < itemContainer.slots.Length; i++) if (itemContainer.slots[i].Add(item)) return true;
        return false;
    }
    public void AddAll(Item item,int count) //Tạm thời chưa sử dụng
    {
        for (int i = 0; i < itemContainer.slots.Length; i++)
        {
            if (itemContainer.slots[i].IsAddable(item))
            {
                itemContainer.slots[i].SetData(item, count);
                break;
            }
        
        }
    }
    public void UnEquipAllItem(Item item, int count)
    {
        for (int i = 0; i < itemContainer.slots.Length; i++)
        {
            if (itemContainer.slots[i].IsAddable(item))
            {
                itemContainer.slots[i].UnEQuip(item, count);
                break;
            }

        }
    }
}
