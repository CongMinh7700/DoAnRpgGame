using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "SO/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    [TextArea(5,5)] public string description;
    public int experienceReward;
    public int goldReward;
    [TextArea] public string[] dialogues;
    [TextArea] public string[] dialoguesComplete;
    [TextArea] public string[] dialoguesInProgress;
    public QuestType type;
    public QuestState questState;
    public string targetName;
    public int targetCount;
    public int currentCount;
    //public List<Item> rewardItem;
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
