using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillCtrl : RPGMonoBehaviour
{
    [SerializeField] protected Despawn skillDespawn;
    public Despawn SkillDespawn => skillDespawn;

    [SerializeField] protected DamageSender skillDamageSender;
    public DamageSender SkillDamageSender => skillDamageSender;
    [SerializeField] protected Transform shooter;
    public Transform Shooter => shooter;


    protected override void LoadComponents()
    {
        this.LoadSkillDamageSender();
        this.LoadSkillDespawn();


    }
    protected virtual void LoadSkillDespawn()
    {
        if (this.skillDespawn != null) return;
        this.skillDespawn = GetComponentInChildren<Despawn>();
    }
    protected virtual void LoadSkillDamageSender()
    {
        if (this.skillDamageSender != null) return;
        this.skillDamageSender = GetComponentInChildren<DamageSender>();

    }
    public virtual void SetShooter(Transform shooter)
    {
        this.shooter = shooter;
    }

}
