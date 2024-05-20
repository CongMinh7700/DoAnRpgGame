using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotDataManager : RPGMonoBehaviour
{
    public CharacterStats characterStats;
    public List<Item> itemList = new List<Item>();
    public ItemManager itemManager;

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
            Transform slotHolder = characterStats.mainContainerUI.Find("SlotHolder");
            SlotInfo info = new SlotInfo();
            for (int i = 0; i < slotHolder.childCount; i++)
            {
                EquipSlot slot = slotHolder.GetChild(i).GetComponent<EquipSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i,GetItemIndex(slot.slotItem));
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
            Debug.Log("Loaded Skill JSON Data:\n" + jsonData);
            Transform slotHolder = characterStats.mainContainerUI.Find("SlotHolder");
            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Item item = GetItemByIndex(info.itemIndexs[i]);
                slotHolder.GetChild(info.slotIndexs[i]).GetComponent<EquipSlot>().SetData(item,1);
            }
            Debug.Log("<color=green>Data succesfully loaded! </color>");
            itemManager.UpdateStats();
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
        return Application.persistentDataPath + $"/Equip{id}.dat";
    }
    public Item GetItemByIndex(int index)
    {
        return itemList[index];
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < itemList.Count; i++) if (itemList[i] == item) return i;
        return -1;
    }
    public class SlotInfo
    {
        public List<int> slotIndexs;
        public List<int> itemIndexs;


        public SlotInfo()
        {
            slotIndexs = new List<int>();
            itemIndexs = new List<int>();
        }

        public void AddInfo(int slotInex, int itemIndex)
        {
            slotIndexs.Add(slotInex);
            itemIndexs.Add(itemIndex);
        }

    }
    #endregion
}
