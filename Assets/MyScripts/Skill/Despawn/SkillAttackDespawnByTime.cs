using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackDespawnByTime : DespawnByTime
{
    public override void DespawnObject()
    {
        SkillSpawner.Instance.Despawn(transform.parent);
    }
}
