using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "SO/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string description;
    public bool isCompleted;

    public string targetEnemy;
    public int targetKillCount;
    public int currentKillCount;
    public int experienceReward;
    public int goldReward;
    [TextArea]
    public string[] dialogues;
    
}
