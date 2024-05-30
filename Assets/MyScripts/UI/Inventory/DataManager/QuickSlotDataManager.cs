using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotDataManager : RPGMonoBehaviour
{
    [SerializeField] protected QuickBar quickBar;
    public List<Item> itemList = new List<Item>();

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
            
            SlotInfo info = new SlotInfo();
            //Add QuickItemSlot
            Transform quickItemSlotHolder = quickBar.mainContainerUI.Find("SlotHolder").Find("QuickItemSlot");
            for (int i = 0; i < quickItemSlotHolder.childCount; i++)
            {
                QuickItemSlot slot = quickItemSlotHolder.GetChild(i).GetComponent<QuickItemSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i, GetItemIndex(slot.slotItem), slot.itemCount, "QuickItemSlot");
                }
            }
            Transform quickSkillSlotHolder = quickBar.mainContainerUI.Find("SlotHolder").Find("SkillSlot");
            for (int i = 0; i < quickSkillSlotHolder.childCount; i++)
            {
                QuickSkillSlot slot = quickSkillSlotHolder.GetChild(i).GetComponent<QuickSkillSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i, GetItemIndex(slot.slotItem), slot.itemCount, "QuickSkillSlot");
                }
            }
            string jsonData = JsonUtility.ToJson(info);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data QuickSlot succesfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the QuickSlot item list");
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

            Transform quickSlotItemHolder = quickBar.mainContainerUI.Find("SlotHolder").Find("QuickItemSlot");
            Transform quickSkillSlotHolder = quickBar.mainContainerUI.Find("SlotHolder").Find("SkillSlot");
            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Item item = GetItemByIndex(info.itemIndexs[i]);
                string slotType = info.slotTypes[i];

                if (slotType == "QuickItemSlot")
                {
                    quickSlotItemHolder.GetChild(info.slotIndexs[i]).GetComponent<QuickItemSlot>().SetData(item, info.itemCounts[i]);
                }
                else if (slotType == "QuickSkillSlot")
                {
                    quickSkillSlotHolder.GetChild(info.slotIndexs[i]).GetComponent<QuickSkillSlot>().SetData(item, info.itemCounts[i]);
                }
            }
            Debug.Log("<color=green>Data Quick Slot succesfully loaded! </color>");
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
        return Application.persistentDataPath + $"/QuickSlot {id}.dat";
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
        public List<int> itemCounts;
        public List<string> slotTypes; // Add this to store slot type information

        public SlotInfo()
        {
            slotIndexs = new List<int>();
            itemIndexs = new List<int>();
            itemCounts = new List<int>();
            slotTypes = new List<string>(); 
        }

        public void AddInfo(int slotIndex, int itemIndex, int itemCount, string slotType)
        {
            slotIndexs.Add(slotIndex);
            itemIndexs.Add(itemIndex);
            itemCounts.Add(itemCount);
            slotTypes.Add(slotType); 
        }
    }

    #endregion
}
