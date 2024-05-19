using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UsingSkill : RPGMonoBehaviour
{
    [SerializeField] protected int manaMax;
    [SerializeField] protected float currentMana;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public int ManaMax => manaMax;
    public float CurrentMana => currentMana;
    public static bool canUseSkill;
    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        this.manaMax = playerCtrl.playerSO.mana;
        this.currentMana = manaMax;
        
    }
    protected virtual void FixedUpdate()
    {
            ManaRecover();
           
            if (QuickSkillSlot.canUsingFireBall)
            {
                this.FireBall();
                QuickSkillSlot.canUsingFireBall = false;
            }
            if (QuickSkillSlot.canUsingHeal)
            {
                this.Heal();
                QuickSkillSlot.canUsingHeal = false;
            }
            if (QuickSkillSlot.canUsingStrength)
            {
                this.Strength();
                QuickSkillSlot.canUsingStrength = false;
            }
    }
   public virtual void FireBall()
    {

        if (currentMana >=20)
        {
            canUseSkill = true;
            ManaDeduct(20);
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            string prefabName = SkillSpawner.fireBall;
            Transform newFireBall = SkillSpawner.Instance.Spawn(prefabName, position, rotation);
            if (newFireBall == null) return;
            newFireBall.gameObject.SetActive(true);
            AttackSkillCtrl skillCtrl = newFireBall.GetComponent<AttackSkillCtrl>();
            skillCtrl.SetShooter(transform);
        }
        else
        {
            canUseSkill = false;
            Debug.Log("Cant use FireBall");
        }
    } 
    public virtual void Heal()
    {
       
        if (currentMana > 50)
        {
            canUseSkill = true;
            ManaDeduct(50);
            Vector3 position = transform.parent.position;
            Quaternion rotation = transform.parent.rotation;
            string prefabName = FxSpawner.heal;
            Transform newHeal = FxSpawner.Instance.Spawn(prefabName, position, rotation);
            if (newHeal == null) return;
            newHeal.gameObject.SetActive(true);
            EffectSkillCtrl skillCtrl = newHeal.GetComponent<EffectSkillCtrl>();
            skillCtrl.SetPositionEF(transform);
        }
        else
        {
            canUseSkill = false;
            Debug.Log("Cant use Heal");
        }
    }
    public virtual void Strength()
    {
        
        if (currentMana > 30)
        {
            canUseSkill = true;
            ManaDeduct(30);
            Vector3 position = transform.parent.position;
            Quaternion rotation = transform.parent.rotation;
            string prefabName = FxSpawner.strength;
            Transform newStrength = FxSpawner.Instance.Spawn(prefabName, position, rotation);
            if (newStrength == null) return;
            newStrength.gameObject.SetActive(true);
            EffectSkillCtrl skillCtrl = newStrength.GetComponent<EffectSkillCtrl>();
            skillCtrl.SetPositionEF(transform);
        }
        else
        {
            canUseSkill = false;
            Debug.Log("Cant use Strength");
        }
        

    }
    public virtual void ManaRecover()
    {
        this.currentMana += 0.1f;
        if (this.currentMana > manaMax) currentMana = manaMax;
    }
    public virtual void ManaDeduct(int value)
    {
        canUseSkill = true;
        if (currentMana < value)
        {
            canUseSkill = false;
        }

        this.currentMana -= value;
        if (currentMana < 0) currentMana = 0;
    }
    public virtual void SetManaMax(int staminaMax)
    {
        this.manaMax = staminaMax;
        if (this.currentMana >= staminaMax) currentMana = staminaMax;
    }

}
