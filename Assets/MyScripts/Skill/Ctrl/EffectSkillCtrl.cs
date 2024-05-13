using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSkillCtrl : RPGMonoBehaviour
{
    [SerializeField] protected Transform position;
    public Transform Position => position;
    [SerializeField] protected SkillDespawnByTime skillDespawnByTime;
    public SkillDespawnByTime SkillDespawnByTime => SkillDespawnByTime;

    protected override void LoadComponents()
    {
        LoadDespawn();
    }
    protected virtual void LoadDespawn()
    {
        if (skillDespawnByTime != null) return;
        this.skillDespawnByTime = GetComponentInChildren<SkillDespawnByTime>();
    }

    public virtual void SetPositionEF(Transform position)
    {
        this.position = position;
    }
}
