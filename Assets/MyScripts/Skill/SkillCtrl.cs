using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtrl : RPGMonoBehaviour
{
    [SerializeField] protected SkillDespawn skillDespawn;
    public SkillDespawn SkillDespawn => skillDespawn;

    [SerializeField] protected SkillDamageSender skillDamageSender;
    public SkillDamageSender SkillDamageSender => skillDamageSender;
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
        this.skillDespawn = GetComponentInChildren<SkillDespawn>();
    }
    protected virtual void LoadSkillDamageSender()
    {
        if (this.skillDamageSender != null) return;
        this.skillDamageSender = GetComponentInChildren<SkillDamageSender>();

    }
    public virtual void SetShooter(Transform shooter)
    {
        this.shooter = shooter;
    }

}
