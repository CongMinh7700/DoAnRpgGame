using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : RPGMonoBehaviour
{
    [Header("SpawnRandom")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomDelay = 3f;
    [SerializeField] protected float randomLimit = 4f;



    protected override void LoadComponents()
    {
        this.LoadSpawnerCtrl(); 
    }
    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + "|LoadSpawnerCtrl|", gameObject);
    }
    protected virtual void FixedUpdate()
    {
        if (this.spawnerCtrl.SpawnTrigger.canSpawn)
            EnemySpawning();
    }
    protected virtual void EnemySpawning()
    {
        if (RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform randomPoint = spawnerCtrl.SpawnPoint.GetRandomPoint();
        Transform prefab = spawnerCtrl.Spawner.GetRandomPrefabs();

        Vector3 position = randomPoint.position;
        Quaternion rotation = randomPoint.rotation;

        Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, position, rotation);
        obj.gameObject.SetActive(true);

    }


    protected virtual bool RandomReachLimit()
    {
        int currentObjs = this.spawnerCtrl.Spawner.SpawnedCount;
        return currentObjs >= this.randomLimit;
    }
    
}
