using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByMana : Despawn
{
    [SerializeField] protected bool pauseTime = false;
    public override void Despawning()
    {

        if (!CanDespawn()) return;
        StartCoroutine(WaitToDespawn());
        if (pauseTime)
        {
            this.DespawnObject();
            pauseTime = false;
            UsingSkill.manaLow = false;
        }
    }
    protected override bool CanDespawn()
    {
        Debug.Log("ManaLow : " + UsingSkill.manaLow);
        return UsingSkill.manaLow;
    }
    IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(1f);
        pauseTime = true;

    }
}
