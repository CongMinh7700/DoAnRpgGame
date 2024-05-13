using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UsingSkill : RPGMonoBehaviour
{
    [SerializeField] protected int manaMax;
    [SerializeField] protected int currentMana;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public int ManaMax => manaMax;
    public int CurrentMana => currentMana;
    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
    }
    protected virtual void FixedUpdate()
    {
        
        if (QuickSkillSlot.isUsingFireBall)
        {
            this.FireBall();
            QuickSkillSlot.isUsingFireBall = false;
        }
        if (QuickSkillSlot.canUsingHeal)
        {
            this.Heal();
            QuickSkillSlot.canUsingHeal = false;
        }
        
   
    }
   public virtual void FireBall()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        string prefabName = SkillSpawner.fireBall;

        Transform newFireBall = SkillSpawner.Instance.Spawn(prefabName, position, rotation);
        if (newFireBall == null) return;
        newFireBall.gameObject.SetActive(true);
        AttackSkillCtrl skillCtrl = newFireBall.GetComponent<AttackSkillCtrl>();
        skillCtrl.SetShooter(transform);
    } 
    public virtual void Heal()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        string prefabName = FxSpawner.heal;
        Transform newHeal = FxSpawner.Instance.Spawn(prefabName, position, rotation);
        if (newHeal == null) return;
        newHeal.gameObject.SetActive(true);
        EffectSkillCtrl skillCtrl = newHeal.GetComponent<EffectSkillCtrl>();
        skillCtrl.SetPositionEF(transform);
    }
    public virtual void ManaRecover(int value)
    {
        this.currentMana += value;
        if (this.currentMana > manaMax) currentMana = manaMax;
    }
    public virtual void ManaDeduct(int value)
    {
        this.currentMana -= value;
        if (currentMana < 0) currentMana = 0;
    }
    public virtual void SetManaMax(int staminaMax)
    {
        this.manaMax = staminaMax;
        if (this.currentMana >= staminaMax) currentMana = staminaMax;
    }

}
