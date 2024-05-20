using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSkillCtrl : RPGMonoBehaviour
{
    [SerializeField] protected Transform position;
    public Transform Position => position;
    [SerializeField] protected DespawnByMana despawnByMana;
    public DespawnByMana DespawnByMana => despawnByMana;

    protected override void LoadComponents()
    {
        LoadDespawn();
    }
    protected virtual void LoadDespawn()
    {
        if (despawnByMana != null) return;
        this.despawnByMana = GetComponentInChildren<DespawnByMana>();
    }

    public virtual void SetPositionEF(Transform position)
    {
        this.position = position;
    }
}
