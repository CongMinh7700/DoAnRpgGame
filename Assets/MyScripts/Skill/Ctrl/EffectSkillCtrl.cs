using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSkillCtrl : RPGMonoBehaviour
{
    [SerializeField] protected Transform position;
    public Transform Position => position;
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn => despawn;

    protected override void LoadComponents()
    {
        LoadDespawn();
    }
    protected virtual void LoadDespawn()
    {
        if (despawn != null) return;
        this.despawn = GetComponentInChildren<Despawn>();
    }

    public virtual void SetPositionEF(Transform position)
    {
        this.position = position;
    }
}
