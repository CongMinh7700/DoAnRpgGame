using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Quest :MonoBehaviour
{
    public List<Goal> goals = new List<Goal>();
    public string questName;
    public string description;
    public int experienceReward;
    public Item itemReward;
    public bool completed;

    public void CheckGoal()
    {
        completed = goals.All(g => g.completed);
        if (completed) GiveReward();
    }
    public void GiveReward()
    {
        if (itemReward != null)
            ItemContainer.Instance.inventoryEvents.AddItem(itemReward);
        //gold,experience
    }
    
}
