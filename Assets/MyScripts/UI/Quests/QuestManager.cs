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

    //Quest
    public TextMeshProUGUI detailQuestTitleText;
    public TextMeshProUGUI detailQuestDescriptionText;
    public TextMeshProUGUI detailQuestStateText;
    public TextMeshProUGUI detailQuestKillCountText;
    public TextMeshProUGUI detailRewardText;
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

    public void OnEnemyKilled(string enemyName)
    {
        Debug.Log("call OnEnemyKilled");
        foreach (Quest quest in activeQuests)
        {
            Debug.Log(quest.questTitle +"Name");
            if (quest.targetEnemy == enemyName && quest.questState != QuestState.Complete)
            {
                quest.currentKillCount++;
                Debug.Log($"Killed {quest.currentKillCount}/{quest.targetKillCount} {quest.targetEnemy}");
                if (quest.currentKillCount >= quest.targetKillCount)
                {
                    CompleteQuest(quest);
                }
            }
        }
    }
    public void AddQuest( Quest newQuest)
    {
        newQuest.questState = QuestState.InProgress;
        activeQuests.Add(newQuest);
        Debug.Log("Add complete Quest");

    }
    public void RemoveQuest(Quest quest)
    {
        if(quest.questState == QuestState.Complete)
        {
            activeQuests.Remove(quest);
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
    public void QuestModify(Quest quest)
    {
        
            if(quest.questState == QuestState.InProgress)
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
        detailQuestTitleText.text =   quest.questTitle;
        detailQuestDescriptionText.text = quest.description;
        detailQuestStateText.text = quest.questState.ToString();
        detailQuestKillCountText.text = $"Tiến độ: {quest.currentKillCount}/{quest.targetKillCount}";
        detailRewardText.text = "Kinh Nghiệm : "+quest.experienceReward+" Exp" +"\nTiền : "+quest.goldReward +" $" ;
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
