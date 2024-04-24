using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    protected static EnemySpawner instance;
    public static EnemySpawner Instance => instance;


    protected override void Awake()
    {
        if (EnemySpawner.instance != null) Debug.LogWarning("Only 1 EnemySpawner allow to exist");
        EnemySpawner.instance = this;
    }
    public override Transform Spawn(Transform prefab, Vector3 position, Quaternion rotation)
    {
        return base.Spawn(prefab, position, rotation);
    }
}
