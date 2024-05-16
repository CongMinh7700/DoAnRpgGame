using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DespawnByHp
{
    [SerializeField] protected static EnemyDespawn instance;
    public static EnemyDespawn Instance => instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();

    }
    
    public override void DespawnObject()
    {

        EnemySpawner.Instance.Despawn(transform.parent);

    }

}
