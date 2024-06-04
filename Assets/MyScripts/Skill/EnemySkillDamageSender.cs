using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDamageSender : DamageSender
{

    [SerializeField] protected AttackSkillCtrl skillCtrl;
    //true,false hoặc lấy attribute
    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
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
        CreateHitEffect();
        this.DestroySkill();
    }
    protected virtual void DestroySkill()
    {
        this.skillCtrl.SkillDespawn.DespawnObject();
    }
    protected virtual void CreateHitEffect()
    {
        string fxName = "";
        fxName = FXSpawner.fireHitEffect;
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        fxObj.gameObject.SetActive(true);
    }
}
