using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : RPGMonoBehaviour
{
    [Header("Spawner Controller")]
    [SerializeField] protected Spawner spawner;
    [SerializeField] protected SpawnPoint spawnPoints;
    [SerializeField] protected SpawnTrigger spawnTrigger;

    public Spawner Spawner => spawner;
    public SpawnPoint SpawnPoint => spawnPoints;
    public SpawnTrigger SpawnTrigger => spawnTrigger;
    protected override void LoadComponents()
    {
        this.LoadSpawner();
        this.LoadSpawnPoint();
        this.LoadSpawnTrigger();
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponentInChildren<Spawner>();
        Debug.LogWarning(transform.name + "|LoadSpawner|", gameObject);
    }
    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints = GetComponentInChildren<SpawnPoint>();
        Debug.LogWarning(transform.name + "|LoadSpawnPoint|", gameObject);
    }
    protected virtual void LoadSpawnTrigger()
    {
        if (this.spawnTrigger != null) return;
        this.spawnTrigger = GetComponentInChildren<SpawnTrigger>();
        Debug.LogWarning(transform.name + "|LoadSpawnTrigger|", gameObject);
    }
}
