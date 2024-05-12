using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        SkillSpawner.Instance.Despawn(transform.parent);
    }

}
