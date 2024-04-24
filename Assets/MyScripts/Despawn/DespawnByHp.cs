using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByHp : Despawn
{
    [SerializeField] protected HitableObjectDamageReceiver damageReceiver;

    protected override bool CanDespawn()
    {
        return damageReceiver.IsDead() ;
    }

    protected override void LoadComponents()
    {
        this.LoadDamageReceiver();
    }
    protected virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.parent.GetComponentInChildren<HitableObjectDamageReceiver>();
        Debug.LogWarning(transform.name + "|LoadDamageReceiver|", gameObject);
    }
}
