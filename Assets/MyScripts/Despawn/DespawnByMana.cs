using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByMana : Despawn
{


    protected override bool CanDespawn()
    {
        Debug.Log("ManaLow : " + UsingSkill.manaLow);
        return UsingSkill.manaLow;
    }
}
