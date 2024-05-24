using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : RPGMonoBehaviour
{

    protected ItemContainer itemContainer;
    protected override void LoadComponents()
    {

        LoadItemContainer();

    }
    public virtual void LoadItemContainer()
    {
        if (this.itemContainer != null) return;
        this.itemContainer = FindObjectOfType<ItemContainer>();
    }
        
    #region Saving & Loading Data

    public void SaveData(string id)
    {

        string dataPath = GetIDPath(id);

        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Exisiting data with id: " + id + "  is overwritten.");
        }

        try
        {
            Transform slotHolder = itemContainer.mainContainerUI.Find("SlotHolder");
            SlotInfo info = new SlotInfo();
            for (int i = 0; i < slotHolder.childCount; i++)
            {
                ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i, ItemManager.Instance.GetItemIndex(slot.slotItem), slot.itemCount);
                }
            }
            string jsonData = JsonUtility.ToJson(info);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data succesfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list");
        }
    }

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
            string jsonData = System.IO.File.ReadAllText(dataPath);
            SlotInfo info = JsonUtility.FromJson<SlotInfo>(jsonData);

            Transform slotHolder = itemContainer.mainContainerUI.Find("SlotHolder");
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

    public void DeleteData(string id)
    {
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data with id: " + id + " is deleted.");
        }
    }

    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/{id}.dat";
    }

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
}
