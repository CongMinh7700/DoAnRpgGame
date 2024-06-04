using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : Spawner
{
    [SerializeField] protected static SkillSpawner instance;
    public static SkillSpawner Instance => instance;

    public static string fireBall = "FireBall";
    public static string iceShard = "IceShard";
    public static string enemyFireBall = "EnemyFireBall";
    public static string demonFireBall = "DemonFireBall";


    protected override void Awake()
    {
        if (SkillSpawner.instance != null) Debug.LogWarning("Only 1 Skill Spawner allow to exist");
        SkillSpawner.instance = this; 

    }
}
