using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

//Inventory
public class ItemContainer : RPGMonoBehaviour
{
    [Header("Player Looter")]
    public Interactor containerInteractor;
    [SerializeField] protected KeyCode UIToggleKey = KeyCode.I;
    [SerializeField] private SlotOptions[] customOptionsMenuConfig;

    public bool dropItemGameObjects = true;
    protected ItemSlot[] slots;
    //mainUI
    protected Transform mainContainerUI;

    protected GameObject itemInfoPanel;
    protected GameObject slotOptionMenu;
    protected bool isContainerUIOpen = false;
    protected bool isUIInitialized;
    protected Transform containerPanel;

    private List<SlotOptionButtonInfo> slotOptionButtonInfosList;


    protected override void OnEnable()
    {
        ItemSlotUIEvents.OnSlotDrag += CloseSlotOptionMenu;
        ContainerPanelDragger.OnContainerPanelDrag += CloseSlotOptionMenu;
    }
    protected override void OnDisable()
    {
        ItemSlotUIEvents.OnSlotDrag -= CloseSlotOptionMenu;
        ContainerPanelDragger.OnContainerPanelDrag -= CloseSlotOptionMenu;
    }
    //Sinh slot <21
   protected override void Awake()
    {
        isUIInitialized = false;
        InitializeContainer();

        foreach(ItemSlot slot in slots)
        {
            slot.Initialize();
        }
    }
    protected virtual void Update()
    {
        if (isUIInitialized == false) return;
        CheckForUIToggleInput();
    }
    //Sinh ra 4 option 
    protected virtual void InitializeContainer()
    {
        InitializeMainUI(transform);    
        CreateSlotOptionMenu(InteractionSettings.Current.internalSlotOptions, containerInteractor);
        isUIInitialized = true;
    }
    protected void InitializeMainUI(Transform containerPanel)
    {
        this.containerPanel = containerPanel;
        mainContainerUI = this.containerPanel.Find("MainUI");
       // mainContainerUI.Find("Title").GetComponentInChildren<Text>().text = "Inventory";
        slotOptionMenu = this.containerPanel.Find("SlotOptions").gameObject;


        Button containerCloseButton = mainContainerUI.transform.Find("CloseButton").GetComponent<Button>();
        containerCloseButton.onClick.AddListener(() => ToggleUI());

        Transform slotHolder = mainContainerUI.Find("SlotHolder");

        slots = new ItemSlot[slotHolder.childCount];

        for(int i=0;i< slots.Length; i++)
        {
            ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
            slots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(delegate { OnSlotClicked(slot, containerInteractor); });
        }
        mainContainerUI.gameObject.SetActive(false);
    }
    protected virtual void CreateSlotOptionMenu(SlotOptions[] config, Interactor interactor)
    {
        if (customOptionsMenuConfig != null && customOptionsMenuConfig.Length > 0)
        {
            config = customOptionsMenuConfig;
        }
        itemInfoPanel = slotOptionMenu.transform.Find("InfoPanel").gameObject;

        GameObject buttonPrefab = InteractionSettings.Current.optionsMenuButtonPrefabs;
        slotOptionButtonInfosList = new List<SlotOptionButtonInfo>();

        foreach(Transform child in slotOptionMenu.transform)
        {
            if (child.GetComponent<Button>()) Destroy(child.gameObject);

        }
        foreach(SlotOptions option in config)
        {
            Button button = Instantiate(buttonPrefab, slotOptionMenu.transform).GetComponent<Button>();
            string buttonTitle = option.ToString();

            Action<ItemSlot, Interactor> onButtonClicked = null;

            switch (option)
            {
                case SlotOptions.Use:
                    buttonTitle = "Use";
                    onButtonClicked = OnUseItemClicked;
                    break;
                case SlotOptions.ItemInfo:
                    buttonTitle = "Info.";
                    onButtonClicked = OnItemInfoClicked;
                    break;
                case SlotOptions.Remove:
                    buttonTitle = "Remove";
                    onButtonClicked = OnRemoveItemClicked;
                    break;
                case SlotOptions.RemoveAll:
                    buttonTitle = "Bulk Remove";
                    onButtonClicked = OnBulkRemoveItemClicked;
                    break;
                case SlotOptions.TransferToInventory:
                    buttonTitle = "Transfer";
                    onButtonClicked = OnTransferToInventoryClicked;
                    break;
                   
            }
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonTitle;
            SlotOptionButtonInfo buttonInfo = new SlotOptionButtonInfo(button, onButtonClicked, OnSlotButtonEventFinished);
            slotOptionButtonInfosList.Add(buttonInfo);
        }
        CloseSlotOptionMenu();
    }
    private void OnTransferToInventoryClicked(ItemSlot slot,Interactor interactor)
    {
        Utils.TransferItemQuantity(slot, interactor.inventory, slot.itemCount);
    }

    protected void OnSlotClicked(ItemSlot slot,Interactor interactor)
    {
        if (slot.IsEmpty) return;

        if (!slotOptionMenu.activeSelf)
        {
            foreach(SlotOptionButtonInfo buttonInfo in slotOptionButtonInfosList)
            {
                buttonInfo.UpdateInfo(slot, interactor);
            }
            OpenSlotOptionMenu();
        }
        else
        {
            CloseSlotOptionMenu();
        }
    }
    private void OpenSlotOptionMenu()
    {
        slotOptionMenu.SetActive(false);
        slotOptionMenu.transform.position = Input.mousePosition;
        StartCoroutine(Utils.TweenScaleIn(slotOptionMenu, 50, Vector3.one));

    }
    private void CloseSlotOptionMenu()
    {
        slotOptionMenu.SetActive(false);
        itemInfoPanel.SetActive(false);
    }
    private void OnRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        if (dropItemGameObjects) slot.RemoveAndDrop(1, containerInteractor.ItemDropPosition);
        else slot.Remove(1);
    }

    private void OnBulkRemoveItemClicked(ItemSlot slot, Interactor interactor)
    {
        if (dropItemGameObjects) slot.RemoveAndDrop(slot.itemCount, interactor.ItemDropPosition);
        else slot.Remove(slot.itemCount);
    }

    private void OnUseItemClicked(ItemSlot slot, Interactor interactor)
    {
        ItemManager.Instance.UseItem(slot);
    }

    private void OnItemInfoClicked(ItemSlot slot, Interactor interactor)
    {
        itemInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = slot.slotItem.itemInformation;
        itemInfoPanel.SetActive(!itemInfoPanel.activeSelf);
    }

    private void OnSlotButtonEventFinished(ItemSlot slot)
    {
        if (slot.IsEmpty)
        {
            CloseSlotOptionMenu();
        }
    }
    protected void CheckForUIToggleInput()
    {
        if (Input.GetKeyDown(UIToggleKey)) ToggleUI();
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++) if (slots[i].Add(item)) return true;
        return false;
    }
    //Khong dung
    public bool ContainsItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i].slotItem == item) return true;
        return false;
    }
    public bool ContainsItemQuantity(Item item, int amount)
    {
        int count = 0;
        foreach (ItemSlot slot in slots)
        {
            if (slot.slotItem == item) count += slot.itemCount;
            if (count >= amount) return true;
        }
        return false;
    }
    //
    //Hiệu ứng to dần của các button.
    protected void ToggleUI()
    {
        CloseSlotOptionMenu();

        //Tweens in/out the UI.
        if (mainContainerUI.gameObject.activeSelf && isContainerUIOpen)
        {
            isContainerUIOpen = false;
            StartCoroutine(Utils.TweenScaleOut(mainContainerUI.gameObject, 50, false));
        }
        else if (!mainContainerUI.gameObject.activeSelf && !isContainerUIOpen)
        {
            isContainerUIOpen = true;
            StartCoroutine(Utils.TweenScaleIn(mainContainerUI.gameObject, 50, Vector3.one));
        }
    }

    //Hiện tại chưa xài tới
    #region Saving & Loading Data

    //This method saves the container data on an unique file path that is aquired based on the passed in id.
    //This id should be unique for different saves.
    //If a save already exists with the id, the data will be overwritten.
    public void SaveData(string id)
    {
        //An unique file path is aquired here based on the passed in id. 
        string dataPath = GetIDPath(id);

        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Exisiting data with id: " + id + "  is overwritten.");
        }

        try
        {
            Transform slotHolder = mainContainerUI.Find("Slot Holder");
            SlotInfo info = new SlotInfo();
            for (int i = 0; i < slotHolder.childCount; i++)
            {
                ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i, ItemManager.Instance.GetItemIndex(slot.slotItem), slot.itemCount);
                }
            }
            //Convert slot info object to json string and write it to a local file
            string jsonData = JsonUtility.ToJson(info);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data succesfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list");
        }
    }

    //Loads container data that was saved with the passed in id.
    //NOTE: A save file must exist first with the id in order for it to be loaded.
    public void LoadData(string id)
    {
        string dataPath = GetIDPath(id);

        if (!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data exists for the provided id: " + id);
            return;
        }

        try
        {
            //Read and parse json string to slot info object and load all data accordingly.
            string jsonData = System.IO.File.ReadAllText(dataPath);
            SlotInfo info = JsonUtility.FromJson<SlotInfo>(jsonData);

            Transform slotHolder = mainContainerUI.Find("Slot Holder");
            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Item item = ItemManager.Instance.GetItemByIndex(info.itemIndexs[i]);
                slotHolder.GetChild(info.slotIndexs[i]).GetComponent<ItemSlot>().SetData(item, info.itemCounts[i]);
            }
            Debug.Log("<color=green>Data succesfully loaded! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list.");
        }
    }

    //Deletes the save with the passed in id, if one exists.
    public void DeleteData(string id)
    {
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data with id: " + id + " is deleted.");
        }
    }

    //Returns a unique path based on the id.
    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/{id}.dat";
    }

    //This struct contains the data for the container slots; used for saving/loading the container slot data.
    //NOTE: JsonUtility wasn't serializing nested information properly. So i resorted to using seperate list of indexes for each slot.
    public class SlotInfo
    {
        public List<int> slotIndexs;
        public List<int> itemIndexs;
        public List<int> itemCounts;

        public SlotInfo()
        {
            slotIndexs = new List<int>();
            itemIndexs = new List<int>();
            itemCounts = new List<int>();
        }

        public void AddInfo(int slotInex, int itemIndex, int itemCount)
        {
            slotIndexs.Add(slotInex);
            itemIndexs.Add(itemIndex);
            itemCounts.Add(itemCount);
        }

    }
    #endregion

    private class SlotOptionButtonInfo
    {
        internal Button optionButton;
        internal Action<ItemSlot, Interactor> onButtonClicked;
        internal Action<ItemSlot> onButtonEventFinished;


        internal SlotOptionButtonInfo(Button optionButton,Action<ItemSlot,Interactor> onButtonClicked,Action<ItemSlot> onButtonEventFinished)
        {
            this.optionButton = optionButton;
            this.onButtonClicked = onButtonClicked;
            this.onButtonEventFinished = onButtonEventFinished;
        }

        internal void UpdateInfo(ItemSlot slot,Interactor interactor)
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
