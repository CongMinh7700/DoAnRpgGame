using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageSender : DamageSender
{
    [SerializeField] protected WeaponCtrl weaponCtrl;
    //SetDamage
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponCtrl();
    }
    public virtual void LoadWeaponCtrl()
    {
        if (this.weaponCtrl != null) return;
        this.weaponCtrl = GetComponentInParent<WeaponCtrl>();
        Debug.LogWarning(transform.name + "|LoadWeaponCtrl|", gameObject);
    }

    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        
    }
    
}
