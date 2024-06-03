using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : RPGMonoBehaviour
{
    [SerializeField] protected SkillUI skillUI;
    public List<Item> itemList = new List<Item>();

    #region Saving & Loading Data

    public void SaveData(string id)
    {

        string dataPath = GetIDPath(id);

        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Exisiting Skill data with id: " + id + "  is overwritten.");
        }

        try
        {
            Transform slotHolder = skillUI.mainContainerUI.Find("SlotHolder");
            SlotInfo info = new SlotInfo();
            for (int i = 0; i < slotHolder.childCount; i++)
            {
                SkillSlot slot = slotHolder.GetChild(i).GetComponent<SkillSlot>();
                if (!slot.IsEmpty)
                {
                    info.AddInfo(i, GetItemIndex(slot.slotItem));
                }
            }
            string jsonData = JsonUtility.ToJson(info);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data Skill succesfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the Skill item list");
        }
    }

    public void LoadData(string id)
    {
        string dataPath = GetIDPath(id);

        if (!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data Skill exists for the provided id: " + id);
            return;
        }

        try
        {
            string jsonData = System.IO.File.ReadAllText(dataPath);
            SlotInfo info = JsonUtility.FromJson<SlotInfo>(jsonData);
            Debug.Log("Loaded Skill JSON Data:\n" + jsonData);
            Transform slotHolder = skillUI.mainContainerUI.Find("SlotHolder");
            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Item item = GetItemByIndex(info.itemIndexs[i]);
                slotHolder.GetChild(info.slotIndexs[i]).GetComponent<SkillSlot>().Add(item);
            }
            Debug.Log("<color=green>Data Skill succesfully loaded! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the SkillManager item list.");
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
    public static bool HasData(string id)
    {
        string dataPath = Application.persistentDataPath + $"/Skill{id}.dat";
        return System.IO.File.Exists(dataPath);
    }
    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/Skill{id}.dat";
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
