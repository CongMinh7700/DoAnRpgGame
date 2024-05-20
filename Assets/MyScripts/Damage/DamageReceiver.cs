using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class DamageReceiver : RPGMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected int currentHp;
    [SerializeField] protected int hpMax;
    [SerializeField] protected int defense;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isAttacked = false;

    public int CurrentHp => currentHp;
    public int HPMax => hpMax;
    public int Defense => defense;
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
        this.currentHp = this.hpMax;
        this.isDead = false;

    }
    public virtual void Health(int value)
    {
        if (this.isDead) return;
        this.currentHp += value;
        Debug.Log("Heal :" + value);
        if (this.currentHp >= this.hpMax) this.currentHp = this.hpMax;

    }
    public virtual void Deduct(int value)
    {
        if (this.isDead) return;
        this.currentHp -= value ;//* (defense * -1));
        isAttacked = true;
        if (this.currentHp <= 0) this.currentHp = 0;
        this.CheckIsDead();
    }
    public virtual bool IsDead()
    {
        return this.currentHp <= 0;
    }
    public virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.isAttacked = false;
        this.OnDead();
    }
    public virtual void SetHpMax(int maxHp)
    {
        this.hpMax = maxHp;
        if (this.currentHp >= hpMax) currentHp = hpMax;//Mới thêm
    }
    public virtual void SetCurentHp(int hpValue)
    {
        this.currentHp = hpValue;
        if (this.currentHp >= hpMax) currentHp = hpMax;//Mới thêm
    }
    public virtual void SetDefense(int defense)
    {
        this.defense = defense;
    }
    protected abstract void OnDead();
}
