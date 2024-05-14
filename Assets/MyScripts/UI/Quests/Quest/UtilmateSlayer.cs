using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilmateSlayer : Quest
{
    private void Start()
    {
        questName = "UtilmateSlayer";
        description = "Kill 10 goblin";
        // itemReward = //Laays item ra ;

        goals.Add(new KillGoal(this,"Kill 10 golblin",completed,0,10));
        goals.Add(new KillGoal(this, "Kill 10 rake",completed,0,10));
        goals.Add(new KillGoal(this,"Kill 10 cactfish",completed,0,10));

        goals.ForEach(g => g.Init());
    }
}
