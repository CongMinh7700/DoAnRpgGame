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
    // cập nhật lại instance mỗi khi vào vùng spawn
    //Thank you portal :V
    protected override void OnEnable() 
    {
        EnemySpawner.instance = this;
        Debug.Log(this.gameObject.name);
    }
    public override Transform Spawn(Transform prefab, Vector3 position, Quaternion rotation)
    {
        return base.Spawn(prefab, position, rotation);
    }
}
