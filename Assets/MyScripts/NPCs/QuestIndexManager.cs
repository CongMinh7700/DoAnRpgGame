using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIndexManager : RPGMonoBehaviour
{
    [SerializeField] protected QuestGiver questGiver;
    protected override void LoadComponents()
    {
        LoadQuestGiver();
    }
    protected virtual void LoadQuestGiver()
    {
        if (questGiver != null) return;
        this.questGiver = GetComponent<QuestGiver>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    SaveData();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    LoadData();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    DeleteData();//Xóa quest Index
        //}
    }
    //Cho start nó tự save là oke lúc thoát game cũng vậy
    #region Saving & Loading Data
    public void SaveData()
    {
        string id = questGiver.shopNumber.ToString();
        string dataPath = GetIDPath(id);

        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Existing data with id: " + id + " is overwritten.");
        }

        try
        {
            QuestInfo currentQuest = new QuestInfo();
            currentQuest.AddQuestInfo(questGiver.shopNumber, questGiver.questIndex);//,questGiver.quests[questGiver.questIndex].questState);
            string jsonData = JsonUtility.ToJson(currentQuest);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data successfully saved!</color>");
            Debug.Log("Number : " + currentQuest.shopNumber + "QuestIndex : " + currentQuest.questIndex);
        }

        catch (System.Exception ex)
        {
            Debug.LogError("Could not save data! Error: " + ex.Message);
        }
    }

    public void LoadData()
    {
        string id = questGiver.shopNumber.ToString();
        string dataPath = GetIDPath(id);

        if (!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data exists for the provided id: " + id);
            return;
        }

        try
        {
            string jsonData = System.IO.File.ReadAllText(dataPath);
            QuestInfo info = JsonUtility.FromJson<QuestInfo>(jsonData);
            Debug.Log("Number : " + info.shopNumber + "QuestIndex : " + info.questIndex + "Quest State :");//+ info.questState);
            if (info.shopNumber == questGiver.shopNumber)
            {
                questGiver.questIndex = info.questIndex;
                //questGiver.quests[questGiver.questIndex].questState = info.questState;
               // Debug.Log("IN Site Number : " + info.shopNumber + "QuestIndex : " + info.questIndex + "Quest State :" + info.questState);
                Debug.Log("<color=green>Data successfully loaded!</color>");
            }
            else
            {
                Debug.LogWarning("Loaded data does not match the shop number.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Could not load data! Error: " + ex.Message);
        }
    }
    public void DeleteDataByID(string id)
    {
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data quest Index Manager with id : " + id + " is deleted.");
        }
    }
    public void DeleteData()
    {
        string id = questGiver.shopNumber.ToString();
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data quest Index Manager with id : " + id + " is deleted.");
        }
    }

    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/QuestIndex{id}.dat";
    }

    public class QuestInfo
    {
        public int shopNumber;
        public int questIndex;
       // public QuestState questState;

        public QuestInfo()
        {
            shopNumber = new int();
            questIndex = new int();
           // questState = new QuestState();
        }
        public void AddQuestInfo(int id, int index)//,QuestState state)
        {
            shopNumber = id;
            questIndex = index;
           // questState = state;
        }
    }
    #endregion
}
