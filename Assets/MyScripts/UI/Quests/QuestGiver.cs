using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public bool assignedQuest;
    public bool helped;

    [SerializeField]private GameObject quests;
    [SerializeField]private string questType;
    public Quest quest;
    void Abcd()
    {
        if(!assignedQuest && !helped)
        {
            AssignQuest();
        }else if (assignedQuest && !helped)
        {
            CheckQuest();
        }
        else
        {
            //Quest isn't complete "Please do it"/,Playername
        }
    }
    void AssignQuest()
    {
        assignedQuest =  true;
      //  Quest = (Quest)quests.AddComponent(questType);
    }
    void CheckQuest()
    {
        if (quest.completed)
        {
            quest.GiveReward();
            helped = true;
            assignedQuest = false;
            //Talk Quest Completed "Thank you very much for that"/,Playername
        }
        else
        {
            //Quest isn't complete "Please do it"/,Playername
        }
    }
}
