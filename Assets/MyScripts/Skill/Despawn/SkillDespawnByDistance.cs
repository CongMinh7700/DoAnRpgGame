using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDespawnByDistance : DespawnByDistance
{
    public override void DespawnObject()
    {
        SkillSpawner.Instance.Despawn(transform.parent);
    }

}
