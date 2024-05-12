using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageSender : DamageSender
{
    [SerializeField] protected SkillCtrl skillCtrl;

    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
    }

    protected virtual void LoadSkillCtrl()
    {
        if (this.skillCtrl != null) return;
        this.skillCtrl = GetComponentInParent<SkillCtrl>();
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
    //MakeFX Like (text)
}
