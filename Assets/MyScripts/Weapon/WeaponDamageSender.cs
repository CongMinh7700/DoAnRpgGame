using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageSender : DamageSender
{
    [SerializeField] protected WeaponCtrl weaponCtrl;
    [SerializeField] protected HitableObjectCtrl hitableObjectCtrl;
    public HitableObjectCtrl HitableObjectCtrl => hitableObjectCtrl;
    //SetDamage
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponCtrl();
        this.LoadHitableObjectCtrl();
        this.damage = ItemManager.bonusAttack;
    }
    public virtual void LoadWeaponCtrl()
    {
        if (this.weaponCtrl != null) return;
        this.weaponCtrl = GetComponentInParent<WeaponCtrl>();
        Debug.LogWarning(transform.name + "|LoadWeaponCtrl|", gameObject);
    }
    public virtual void LoadHitableObjectCtrl()
    {
        if (this.hitableObjectCtrl != null) return;
        this.hitableObjectCtrl = transform.root.GetComponent<HitableObjectCtrl>();
        Debug.LogWarning(transform.name + "|LoadHitableObjectCtrl|", gameObject);
    }
    private void Update()
    {
        //if (ItemManager.isEquippedWeapon == false)
        //{
        //    SetDamage(hitableObjectCtrl.HitableObjectSO.damage);
        //}
        //else
        //{
            SetDamage(hitableObjectCtrl.HitableObjectSO.damage + ItemManager.bonusAttack);
        //}
    }
    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        
    }

}
