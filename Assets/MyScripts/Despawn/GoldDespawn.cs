using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        GoldSpawner.Instance.Despawn(transform.parent);
    }
}
