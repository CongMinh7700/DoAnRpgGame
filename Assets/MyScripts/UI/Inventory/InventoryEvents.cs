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

        //Tweens in/out the UI.
        if (itemContainer.mainContainerUI.gameObject.activeSelf && itemContainer.isContainerUIOpen)
        {
            itemContainer.isContainerUIOpen = false;
            StartCoroutine(Utils.TweenScaleOut(itemContainer.mainContainerUI.gameObject, 50, false));
        }
        else if (!itemContainer.mainContainerUI.gameObject.activeSelf && !itemContainer.isContainerUIOpen)
        {
            itemContainer.isContainerUIOpen = true;
            StartCoroutine(Utils.TweenScaleIn(itemContainer.mainContainerUI.gameObject, 50, Vector3.one));
        }
    }
    public void OnRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        if (itemContainer.dropItemGameObjects) slot.RemoveAndDrop(1, itemContainer.containerInteractor.ItemDropPosition);
        else slot.Remove(1);
    }
    public void OnTransferToInventoryClicked(ItemSlot slot, Interactor interactor)
    {
        Utils.TransferItemQuantity(slot, interactor.inventory, slot.itemCount);
    }
    public void OpenSlotOptionMenu()
    {
        itemContainer.slotOptionMenu.SetActive(false);
        itemContainer.slotOptionMenu.transform.position = Input.mousePosition;
        StartCoroutine(Utils.TweenScaleIn(itemContainer.slotOptionMenu, 50, Vector3.one));

    }
    public void CloseSlotOptionMenu()
    {
        itemContainer.slotOptionMenu.SetActive(false);
        itemContainer.itemInfoPanel.SetActive(false);
    }


    public void OnBulkRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        if (itemContainer.dropItemGameObjects) slot.RemoveAndDrop(slot.itemCount, interactor.ItemDropPosition);
        else slot.Remove(slot.itemCount);
    }

    public void OnUseItemClicked(ItemSlot slot, Interactor interactor)
    {
        ItemManager.Instance.UseItem(slot);
    }
    public void OnItemInfoClicked(ItemSlot slot, Interactor interactor)
    {
        itemContainer.itemInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = slot.slotItem.itemInformation;
        itemContainer.itemInfoPanel.SetActive(!itemContainer.itemInfoPanel.activeSelf);
    }
    public void OnSlotButtonEventFinished(ItemSlot slot)
    {
        if (slot.IsEmpty)
        {
            CloseSlotOptionMenu();
        }
    }
    public void CheckForUIToggleInput()
    {
        if (Input.GetKeyDown(itemContainer.UIToggleKey)) ToggleUI();
        Debug.Log("UIToggle");
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemContainer.slots.Length; i++) if (itemContainer.slots[i].Add(item)) return true;
        return false;
    }

    //Khong dung
    public bool ContainsItem(Item item)
    {
        for (int i = 0; i < itemContainer.slots.Length; i++)
            if (itemContainer.slots[i].slotItem == item) return true;
        return false;
    }
    public bool ContainsItemQuantity(Item item, int amount)
    {
        int count = 0;
        foreach (ItemSlot slot in itemContainer.slots)
        {
            if (slot.slotItem == item) count += slot.itemCount;
            if (count >= amount) return true;
        }
        return false;
    }
}
