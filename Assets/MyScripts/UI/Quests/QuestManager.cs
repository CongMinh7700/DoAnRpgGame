using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : RPGMonoBehaviour
{
    protected static QuestManager instance;
    public static QuestManager Instance => instance;
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> questList = new List<Quest>();
    public Transform questListContent;
    public GameObject questItemPrefab;
    public GameObject mainUI;

    //Quest
    public TextMeshProUGUI detailQuestTitleText;
    public TextMeshProUGUI detailQuestDescriptionText;
    public TextMeshProUGUI detailQuestStateText;
    public TextMeshProUGUI detailQuestKillCountText;
    public TextMeshProUGUI detailRewardText;
    protected bool isQuestNull = false;
    protected override void Awake()
    {
        if (QuestManager.instance != null) Debug.LogWarning("Only 1 Quest Manager Allow to exist");
        QuestManager.instance = this;
    }
    public void CompleteQuest(Quest quest)
    {
        quest.questState = QuestState.Complete;
        Debug.Log(quest.questTitle + " is completed!");
    }

    public void UpdateQuestProgress(string targetName)
    {
        //Debug.Log("call OnEnemyKilled");
        foreach (Quest quest in activeQuests)
        {
            Debug.Log(quest.questTitle + "Name");
            if (quest.targetName == targetName && quest.questState != QuestState.Complete)
            {
                quest.currentCount++;
                Debug.Log($"Progress: {quest.currentCount}/{quest.targetCount} {quest.targetName}");
                if (quest.currentCount >= quest.targetCount)
                {
                    CompleteQuest(quest);
                    UpdateQuestLog();
                }
                else
                {
                    return;
                }
            }
        }
    }
    public bool GetQuestBoss(string targetName)
    {
        foreach (Quest quest in activeQuests)
        {
            Debug.Log(quest.questTitle + "Name");
            if (quest.targetName == targetName && quest.questState == QuestState.InProgress)
            {
                Debug.Log("Call this");
                return true;
            }
        }
        return false;
    }
    private void Update()
    {
        if (!mainUI.activeSelf)
        {
            HideQuestDetails();
            //   Debug.Log("Call Active");
        }
    }
    public void AddQuest(Quest newQuest)
    {
        newQuest.questState = QuestState.InProgress;
        activeQuests.Add(newQuest);
        Debug.Log("Add complete Quest");

    }

    public void RemoveQuest(Quest quest)
    {
        if (quest.questState == QuestState.Complete)
        {
            activeQuests.Remove(quest);
            UpdateQuestLog();
            Debug.Log("Remove Quest");
        }

    }

    public void UpdateQuestLog()
    {
        foreach (Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Quest quest in activeQuests)
        {
            GameObject questItem = Instantiate(questItemPrefab, questListContent);
            QuestItemUI questItemUI = questItem.GetComponent<QuestItemUI>();
            questItemUI.SetQuest(quest);
        }

    }
    public void LoadQuest(Quest quest)
    {
        foreach (Quest activeQuest in activeQuests)
        {
            if (activeQuest.questTitle == quest.questTitle)
            {
                Debug.Log("Quest already in activeQuests");
                return; // Exit the method if the quest already exists
            }
        }
        if (quest.questState == QuestState.InProgress || quest.questState == QuestState.Complete)
        {
            GameObject questItem = Instantiate(questItemPrefab, questListContent);
            QuestItemUI questItemUI = questItem.GetComponent<QuestItemUI>();
            questItemUI.SetQuest(quest);

        }
        activeQuests.Add(quest);

    }
    public void DisplayQuestDetails(Quest quest)
    {
        if (quest == null) return;
        detailQuestTitleText.text = quest.questTitle;
        detailQuestDescriptionText.text = "Mô tả : \n" + quest.description;
        detailQuestStateText.text = quest.questState.ToString();
        detailQuestKillCountText.text = $"Tiến độ: {quest.currentCount}/{quest.targetCount}";
        detailRewardText.text = "Phần Thưởng \nKinh Nghiệm : " + quest.experienceReward + " Exp" + "\nTiền : " + quest.goldReward + " $";
    }
    public void HideQuestDetails()
    {
        detailQuestTitleText.text = "";
        detailQuestDescriptionText.text = "";
        detailQuestStateText.text = "";
        detailQuestKillCountText.text = "";
        detailRewardText.text = "";
    }

    public Quest GetQuestByIndex(int index)
    {
        return questList[index];
    }
    public int GetQuestIndex(Quest quest)
    {
        for (int i = 0; i < questList.Count; i++) if (questList[i] == quest) return i;
        return -1;
    }
}
