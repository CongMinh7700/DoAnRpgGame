using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "SO/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string description;
    public bool isCompleted;

    public string targetEnemy;//Name
    public int targetKillCount;
    public int currentKillCount;
    public int experienceReward;
    public int goldReward;
    [TextArea]
    public string[] dialogues;
    public QuestType type;
    public QuestState questState;
    public List<Item> requiredItem;
    public List<Item> rewardItem;

    public void CompleteQuest()
    {
        isCompleted = true;
        questState = QuestState.Complete;
    }
    public void UpdateQuestProgress(int killCount)
    {
        currentKillCount += killCount;
        if (currentKillCount >= targetKillCount)
        {
            CompleteQuest();
        }
    }
}
public enum QuestType {
    KillQuest = 0,
    BuyQuest = 1,
    CollectQuest = 2,
}
public enum QuestState
{
    NotStarted = 0,
    InProgress = 1,
    Complete = 2,
    Failed = 3,
}