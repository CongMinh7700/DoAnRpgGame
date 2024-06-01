using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : RPGMonoBehaviour
{
    [SerializeField] protected QuestManager questManager;

    protected override void LoadComponents()
    {

        LoadQuestManager();
    }

    public virtual void LoadQuestManager()
    {
        if (this.questManager != null) return;
        this.questManager = GetComponent<QuestManager>();
    }
    public void SaveData(string id)
    {
   
        string dataPath = GetIDPath(id);

        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Exisiting Quest data with id: " + id + "  is overwritten.");
        }

        try
        {
            Transform questListContent = questManager.questListContent.transform;
            SlotInfo info = new SlotInfo();
            for (int i = 0; i < questListContent.childCount; i++)
            {
                QuestItemUI slot = questListContent.GetChild(i).GetComponent<QuestItemUI>();
                info.AddInfo(i, questManager.GetQuestIndex(slot.quest));
            }
            Debug.Log(questListContent.childCount);
            string jsonData = JsonUtility.ToJson(info);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data Quest succesfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the QuestDataManager item list");
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

            Transform questListContent = questManager.questListContent.transform;
            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Quest quest = questManager.GetQuestByIndex(info.itemIndexs[i]);
                questManager.QuestModify(quest);
                questListContent.GetChild(info.slotIndexs[i]).GetComponent<QuestItemUI>().SetQuest(quest);

            }

            Debug.Log("<color=green>Data Quest succesfully loaded! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the QuestManager item list.");
        }
    }
    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/QuestDataManager{id}.dat";
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
}
