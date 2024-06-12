using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDamageSender : WeaponDamageSender
{
    protected int baseDamage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadHitableObjectCtrl();
    }

    private void Start()
    {
        this.damage = hitableObjectCtrl.HitableObjectSO.damage;
    }
    //public override void LoadHitableObjectCtrl()
    //{
    //    if (this.hitableObjectCtrl != null) return;
    //    this.hitableObjectCtrl = transform.root.GetComponent<HitableObjectCtrl>();
    //    Debug.LogWarning(transform.name + "|LoadHitableObjectCtrl|", gameObject);
    //}

    protected virtual void UpdateBase()
    {
        baseDamage = LevelSystem.damageLevel;
    }
    private void Update()
    {
        UpdateBase();
        UpdateDamageSkill();
    }
    protected virtual void UpdateDamageSkill()
    {
        //SetDoublDamage neu bat cuong no
        if (PlayerCtrl.strengthOn)
        {
            SetDamage((baseDamage + ItemManager.bonusAttack) * 2);
        }
        else
        {
            SetDamage(baseDamage + ItemManager.bonusAttack);
        }
    }
}
