﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

//Inventory
public class ItemContainer : RPGMonoBehaviour
{
    [Header("Singleton")]
    protected static ItemContainer instance;
    public static ItemContainer Instance => instance;
    [Header("Inventory")]
    [SerializeField] private SlotOptions[] customOptionsMenuConfig;
    public KeyCode UIToggleKey = KeyCode.I;
    public bool dropItemGameObjects = true;
    public ItemSlot[] slots;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public GameObject itemInfoPanel;
    [HideInInspector] public GameObject slotOptionMenu;
    [HideInInspector] public Interactor containerInteractor;
    [HideInInspector] public InventoryEvents inventoryEvents;
    [HideInInspector] public bool isContainerUIOpen = false;
    protected Transform containerPanel;
    protected bool isUIInitialized;
    private List<SlotOptionButtonInfo> slotOptionButtonInfosList;

    //đăng ký sk cho drag
    //khi drag thì tắt option button
    protected override void OnEnable()
    {
        ItemSlotUIEvents.OnSlotDrag += inventoryEvents.CloseSlotOptionMenu;
        ContainerPanelDragger.OnContainerPanelDrag += inventoryEvents.CloseSlotOptionMenu;
    }
    protected override void OnDisable()
    {
        ItemSlotUIEvents.OnSlotDrag -= inventoryEvents.CloseSlotOptionMenu;
        ContainerPanelDragger.OnContainerPanelDrag -= inventoryEvents.CloseSlotOptionMenu;
    }
    //Sinh slot <21
    protected void LoadInventoryEvents()
    {
        if (this.inventoryEvents != null) return;
        this.inventoryEvents = GetComponent<InventoryEvents>();
    }
    protected virtual void LoadInteractor()
    {
        if (this.containerInteractor != null) return;
        this.containerInteractor = FindObjectOfType<Interactor>();
    }
    protected override void Awake()
    {
        isUIInitialized = false;
        this.LoadInventoryEvents();
        LoadInteractor();
        InitializeContainer();
        foreach (ItemSlot slot in slots)
        {
            slot.Initialize();
        }
    }
    protected virtual void Update()
    {
        if (isUIInitialized == false) return;
        inventoryEvents.CheckForUIToggleInput();

        //   useName = ItemManager.isEquipped ? "Tháo" : "Dùng";
    }
    //Sinh ra 4 option 
    protected virtual void InitializeContainer()
    {
        InitializeMainUI(transform);
        CreateSlotOptionsMenu(InteractionSettings.Current.internalSlotOptions, containerInteractor);//
        isUIInitialized = true;
    }
    protected virtual void InitializeMainUI(Transform containerPanel)
    {
        this.containerPanel = containerPanel;
        mainContainerUI = this.containerPanel.Find("MainUI");
        slotOptionMenu = this.containerPanel.Find("SlotOptions").gameObject;
        GetCloseButton();
        Transform slotHolder = mainContainerUI.Find("SlotHolder");

        slots = new ItemSlot[slotHolder.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
            slots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();//xóa tất cả sk
            //cho phép truyền tham số mà không cần định nghĩa phương thức mới vào hàm gọi lại
            slotButton.onClick.AddListener(delegate { OnSlotClicked(slot, containerInteractor); });
        }
        mainContainerUI.gameObject.SetActive(false);
    }
    protected virtual void GetCloseButton()
    {
        Button containerCloseButton = mainContainerUI.transform.Find("CloseButton").GetComponent<Button>();
        containerCloseButton.onClick.AddListener(() => inventoryEvents.ToggleUI());//them sự kiện đóng vì chỉ khi hiển thị mới có close button
    }
    //Create SlotOption
    protected virtual void CreateSlotOptionsMenu(SlotOptions[] config, Interactor interactor)
    {
        if (customOptionsMenuConfig != null && customOptionsMenuConfig.Length > 0)
        {
            config = customOptionsMenuConfig;
        }
        itemInfoPanel = slotOptionMenu.transform.Find("InfoPanel").gameObject;
        GameObject buttonPrefab = InteractionSettings.Current.optionsMenuButtonPrefabs;
        slotOptionButtonInfosList = new List<SlotOptionButtonInfo>();

        foreach (Transform child in slotOptionMenu.transform)//xóa button cũ để thêm object mới
        {
            if (child.GetComponent<Button>()) Destroy(child.gameObject);

        }

        foreach (SlotOptions option in config)
        {
            Button button = Instantiate(buttonPrefab, slotOptionMenu.transform).GetComponent<Button>();
            string buttonTitle = option.ToString();

            Action<ItemSlot, Interactor> onButtonClicked = null;
                switch (option)
                {
                    case SlotOptions.Use:
                        buttonTitle = "Dùng";
                        onButtonClicked = inventoryEvents.OnUseItemClicked;
                        break;
                    case SlotOptions.ItemInfo:
                        buttonTitle = "Thông tin";
                        onButtonClicked = inventoryEvents.OnItemInfoClicked;
                        break;
                case SlotOptions.Remove:
                        buttonTitle = "Xóa";
                        onButtonClicked = inventoryEvents.OnRemoveItemClicked;
                        break;
                    case SlotOptions.RemoveAll:
                        buttonTitle = "Xóa Hết";
                        onButtonClicked = inventoryEvents.OnBulkRemoveItemClicked;
                        break;
                    case SlotOptions.TransferToInventory:
                        buttonTitle = "Chuyển";
                        onButtonClicked = inventoryEvents.OnTransferToInventoryClicked;
                        break;
                }
            
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonTitle;
            SlotOptionButtonInfo buttonInfo = new SlotOptionButtonInfo(button, onButtonClicked, inventoryEvents.OnSlotButtonEventFinished);
            slotOptionButtonInfosList.Add(buttonInfo);
        }
        inventoryEvents.CloseSlotOptionMenu();
    }

    protected void OnSlotClicked(ItemSlot slot, Interactor interactor)
    {
        if (slot.IsEmpty)
        {
            inventoryEvents.CloseSlotOptionMenu();
            return;
        }
        if (!slotOptionMenu.activeSelf)
        {
            foreach (SlotOptionButtonInfo buttonInfo in slotOptionButtonInfosList)
            {
                buttonInfo.UpdateInfo(slot, interactor);
            }
            inventoryEvents.OpenSlotOptionMenu();
        }
        else
        {
            inventoryEvents.CloseSlotOptionMenu();
        }
    }
   
    private class SlotOptionButtonInfo
    {
        internal Button optionButton;
        internal Action<ItemSlot, Interactor> onButtonClicked;
        internal Action<ItemSlot> onButtonEventFinished;


        internal SlotOptionButtonInfo(Button optionButton, Action<ItemSlot, Interactor> onButtonClicked, Action<ItemSlot> onButtonEventFinished)
        {
            this.optionButton = optionButton;
            this.onButtonClicked = onButtonClicked;
            this.onButtonEventFinished = onButtonEventFinished;
        }

        internal void UpdateInfo(ItemSlot slot, Interactor interactor)
        {
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(
                delegate
                {
                    onButtonClicked?.Invoke(slot, interactor);
                    onButtonEventFinished?.Invoke(slot);
                }
                );
        }

    }
}
