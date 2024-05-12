using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : Spawner
{
    [SerializeField] protected static SkillSpawner instance;
    public static SkillSpawner Instance => instance;

    public string fireBall = "FireBall";


    protected override void Awake()
    {
        if (SkillSpawner.instance != null) Debug.LogWarning("Only 1 Skill Spawner allow to exist");
        SkillSpawner.instance = this; 

    }
}
