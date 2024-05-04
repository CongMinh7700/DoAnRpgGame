using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObjectDamageReceiver : DamageReceiver
{
    [Header("Damage Receiver")]
    [SerializeField] protected HitableObjectCtrl hitableObjectCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }
    public virtual void LoadCtrl()
    {
        if (this.hitableObjectCtrl != null) return;
        this.hitableObjectCtrl = GetComponentInParent<HitableObjectCtrl>();
        this.hpMax = hitableObjectCtrl.HitableObjectSO.hpMax;
        this.defense = hitableObjectCtrl.HitableObjectSO.defense;
        Debug.Log(transform.name + "|LoadHitableObjectCtrl|", gameObject);
    }
    protected override void OnDead()
    {
       //Despawn Object
    }

    
}
