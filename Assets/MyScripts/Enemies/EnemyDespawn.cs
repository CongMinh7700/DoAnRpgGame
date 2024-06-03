using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DespawnByHp
{
    [SerializeField] protected static EnemyDespawn instance;
    public static EnemyDespawn Instance => instance;
    [SerializeField] protected bool pauseTime = false;
    [SerializeField] protected float timeDespawn;
    protected override void LoadComponents()
    {
        base.LoadComponents();

    }
    public override void Despawning()
    {

        if (!CanDespawn()) return;
        StartCoroutine(WaitToDespawn());
        Debug.LogWarning("PauseTime : " + pauseTime);
        if (pauseTime)
        {
    
            this.DespawnObject();
            pauseTime = false;
        }
       
    }
    public override void DespawnObject()
    {

        EnemySpawner.Instance.Despawn(transform.parent);

    }
    IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(timeDespawn);
        pauseTime = true;

    }


}
