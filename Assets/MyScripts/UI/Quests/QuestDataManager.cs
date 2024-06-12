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
            Debug.Log("Existing Quest data with id: " + id + " is overwritten.");
        }

        try
        {
            Transform questListContent = questManager.questListContent.transform;
            SlotInfo info = new SlotInfo();

            for (int i = 0; i < questListContent.childCount; i++)
            {
                QuestItemUI slot = questListContent.GetChild(i).GetComponent<QuestItemUI>();
                if (slot != null && slot.quest != null)
                {
                    int questIndex = questManager.GetQuestIndex(slot.quest);

                    // Ensure index is within range
                    if (i < questManager.activeQuests.Count)
                    {
                        QuestState questState = questManager.activeQuests[i].questState;
                        info.AddInfo(i, questIndex, questState, questManager.activeQuests[i].currentCount);
                      //  Debug.Log($"Slot {i}: QuestIndex = {questIndex}, QuestState = {questState}, QuestTitle = {slot.quest.questTitle}");
                       // Debug.Log($"Quest Manager Quest State: {questManager.activeQuests[i].questState}");
                    }
                    else
                    {
                        Debug.LogWarning($"Index {i} is out of range for activeQuests. Skipping this slot.");
                    }
                }
                else
                {
                    Debug.LogWarning($"QuestItemUI or Quest is null for slot {i}. Skipping this slot.");
                }
            }

            // Convert to JSON
            string jsonData = JsonUtility.ToJson(info, true);
           // Debug.Log($"JSON Data: {jsonData}");

            // Save to file
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data Quest successfully saved! </color>");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Could not save container data: {ex.Message}");
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
        //Check current và set quest
        try
        {
            string jsonData = System.IO.File.ReadAllText(dataPath);
            SlotInfo info = JsonUtility.FromJson<SlotInfo>(jsonData);

            Transform questListContent = questManager.questListContent.transform;

            for (int i = 0; i < info.slotIndexs.Count; i++)
            {
                Quest quest = questManager.GetQuestByIndex(info.itemIndexs[i]);
                quest.currentCount = info.currentCounts[i];
                if (quest.currentCount < quest.targetCount) quest.questState = QuestState.InProgress;
                else if (quest.currentCount >= quest.targetCount) quest.questState = QuestState.Complete;
                questManager.LoadQuest(quest);
                Debug.Log("Quest State Load Quest(Load Data) :" + info.questStates[i]);
                Debug.Log("Number : " + i + "QuestIndex : " + questManager.GetQuestByIndex(info.itemIndexs[i]) +info.itemIndexs[i] + "Quest State :" + quest.questState + "Slot :" + quest.questTitle);
                questListContent.GetChild(info.slotIndexs[i]).GetComponent<QuestItemUI>().LoadSetQuest(quest, quest.currentCount);//SetQuest(quest);//,info.questStates[i]);

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
    public static bool HasData(string id)
    {
        string dataPath = Application.persistentDataPath + $"/QuestDataManager{id}.dat";
        return System.IO.File.Exists(dataPath);
    }
    public class SlotInfo
    {
        public List<int> slotIndexs;
        public List<int> itemIndexs;
        public List<QuestState> questStates;
        public List<int> currentCounts;

        public SlotInfo()
        {
            slotIndexs = new List<int>();
            itemIndexs = new List<int>();
            questStates = new List<QuestState>();
            currentCounts = new List<int>();
        }

        public void AddInfo(int slotInex, int itemIndex,QuestState questState,int currentCount)
        {
            slotIndexs.Add(slotInex);
            itemIndexs.Add(itemIndex);
            questStates.Add(questState);
            currentCounts.Add(currentCount);
        }

    }
}
