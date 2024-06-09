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
    public bool bossSpawned = false;

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
        if (this.spawnerCtrl.SpawnTrigger.CanSpawn)
            EnemySpawning();
    }
    protected virtual void EnemySpawning()
    {
        if (RandomReachLimit()) return;
        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;
        if (SpawnPoint.canSpawn)
        {
            Transform randomPoint = spawnerCtrl.SpawnPoint.GetRandomPoint();
            Transform prefab = spawnerCtrl.Spawner.GetRandomPrefabs();


            ObjectType enemyType = prefab.GetComponent<EnemyCtrl>().HitableObjectSO.objType;
            Debug.Log("EnemyType :" +enemyType);
            if (enemyType == ObjectType.Boss)
            {
                Debug.Log(QuestManager.Instance.GetQuestBoss(prefab.name));
                if (!QuestManager.Instance.GetQuestBoss(prefab.name))
                {
                    return;
                }
                else
                {
                    Debug.Log("SpawnRandom : Can spawn Bosss");
                    if (bossSpawned)
                    {
                        Debug.LogWarning("Boss already spawned, cannot spawn another.");
                        return;
                    }
                    else
                    {
                        bossSpawned = true;
                    }

                }
               
                
            }
            Vector3 position = randomPoint.position;
            Quaternion rotation = randomPoint.rotation;
            Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, position, rotation);
            obj.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Can't Spawn");
        }
    }


    public virtual bool RandomReachLimit()
    {
        int currentObjs = this.spawnerCtrl.Spawner.spawnedCount;
        return currentObjs >= this.randomLimit;
    }

}
