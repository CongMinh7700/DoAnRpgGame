using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : RPGMonoBehaviour
{
    [SerializeField] protected int damage = 10;

    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        Debug.Log("Damage :" + damage);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
       
    }
    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
