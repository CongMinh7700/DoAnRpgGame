using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public KillGoal(Quest quest,string descriptions,bool completed,int currentAmount,int requireAmount)
    {
        this.quest = quest;
        this.descriptions = descriptions;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.requireAmount = requireAmount;
    }
    public override void Init()
    {
        base.Init();
    }

    void EnemyDied(int count)
    {
        //enemyTypeCount ++;
        Evaluate();
    }
}
