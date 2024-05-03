using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class DamageReceiver : RPGMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected int hp;
    [SerializeField] protected int hpMax;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isAttacked = false;

    public int HP => hp;
    public int HPMax => hpMax;
    protected override void LoadComponents()
    {
        this.LoadBoxCollider();
    }
    public virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        Debug.LogWarning(transform.name + "|LoadBoxCollider|", gameObject);
        this.boxCollider.isTrigger = true;
    }
    protected override void OnEnable()
    {
        this.Reborn();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.Reborn();
    }
    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;

    }
    public virtual void Health(int value)
    {
        if (this.isDead) return;
        this.hp += value;
        if (this.hp >= this.hpMax) this.hp = this.hpMax;

    }
    public virtual void Deduct(int value)
    {
        if (this.isDead) return;
        this.hp -= value;
        isAttacked = true;
        if (this.hp <= 0) this.hp = 0;
        this.CheckIsDead();
    }
    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }
    public virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.isAttacked = false;
        this.OnDead();
    }
    public virtual void SetHpMax(int hpMax)
    {
        this.hpMax = hpMax;
        if (this.hp >= hpMax) hp = hpMax;//Mới thêm
    }
    protected abstract void OnDead();
}
