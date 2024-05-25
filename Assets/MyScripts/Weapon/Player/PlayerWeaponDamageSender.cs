using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDamageSender : WeaponDamageSender
{
    protected int baseDamage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHitableObjectCtrl();
    }

    private void Start()
    {
        this.damage = hitableObjectCtrl.HitableObjectSO.damage;
    }
    public virtual void LoadHitableObjectCtrl()
    {
        if (this.hitableObjectCtrl != null) return;
        this.hitableObjectCtrl = transform.root.GetComponent<HitableObjectCtrl>();
        Debug.LogWarning(transform.name + "|LoadHitableObjectCtrl|", gameObject);
    }

    protected virtual void UpdateBase()
    {
        baseDamage = LevelSystem.damageLevel;
    }
    private void Update()
    {
       // Debug.Log("Strength On :" + PlayerCtrl.strengthOn);
      //  Debug.Log(LevelSystem.damageLevel);
        //Debug.Log("Damage : " + damage);
       // Debug.Log("Damage Base : " + baseDamage);
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
