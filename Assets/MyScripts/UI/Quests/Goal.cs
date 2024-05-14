using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal 
{
    public Quest quest;
    public bool completed;
    public string descriptions;
    public int currentAmount;
    public int requireAmount;


    public virtual void Init()
    {

    }
    public void Evaluate()
    {
        if(currentAmount >= requireAmount)
        {
            Completed();
        }
    }
    public void Completed()
    {
        quest.CheckGoal();
        this.completed = true;
    }
}
