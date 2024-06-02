using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageSender : DamageSender
{
    [SerializeField] protected AttackSkillCtrl skillCtrl;
    public bool isFire = false;
    //true,false hoặc lấy attribute
    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
    }
    protected override void OnEnable()
    {
        if(isFire)
            SetDamage(LevelSystem.damageLevel * 2);
        else
            SetDamage(LevelSystem.damageLevel * 3);
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
        if (isFire)
        {
             fxName = FXSpawner.fireHitEffect;
        }
        else
        {
            fxName = FXSpawner.iceHitEffect;
        }
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        fxObj.gameObject.SetActive(true);
    }
}
