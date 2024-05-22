using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageSender : DamageSender
{
    [SerializeField] protected AttackSkillCtrl skillCtrl;
    //true,false hoặc lấy attribute
    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
    }
    protected override void OnEnable()
    {
        SetDamage(LevelSystem.damageLevel * 2);
    }
    protected virtual void LoadSkillCtrl()
    {
        if (this.skillCtrl != null) return;
        this.skillCtrl = GetComponentInParent<AttackSkillCtrl>();
    }

    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        Vector3 hitPos = transform.position;
        //CreateFx
        this.DestroySkill();
    }
    protected virtual void DestroySkill()
    {
        this.skillCtrl.SkillDespawn.DespawnObject();
    }
    //MakeFX Like (text),Effect Impact
}
