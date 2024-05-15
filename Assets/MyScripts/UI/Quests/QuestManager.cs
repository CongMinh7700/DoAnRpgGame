using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    void Start()
    {
        foreach (Quest quest in quests)
        {
            Debug.Log("Quest: " + quest.questTitle);
            Debug.Log("Description: " + quest.description);
        }
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        Debug.Log(quest.questTitle + " is completed!");
    }

    public void OnEnemyKilled(string enemyName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.targetEnemy == enemyName && !quest.isCompleted)
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
}
