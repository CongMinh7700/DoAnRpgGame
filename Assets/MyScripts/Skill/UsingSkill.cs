using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingSkill : RPGMonoBehaviour
{
    [SerializeField] protected int manaMax;
    [SerializeField] protected float currentMana;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public int ManaMax => manaMax;
    public float CurrentMana => currentMana;
    public static bool manaLow;

    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        this.manaMax = playerCtrl.PlayerSO.mana;
        this.currentMana = manaMax;

    }
    protected virtual void FixedUpdate()
    {
        // manaLow = false;
        ManaRecover();
        if (PlayerCtrl.shieldOn)
        {
            ManaDeduct(5 * Time.deltaTime);
            if (currentMana < 1f)
            {
                PlayerCtrl.shieldOn = false;
                manaLow = true;
            }
        }
        if (PlayerCtrl.strengthOn)
        {
            ManaDeduct(5 * Time.deltaTime);
            if (currentMana < 1f)
            {
                PlayerCtrl.strengthOn = false;
                manaLow = true;
            }
        }
    }
    public virtual bool FireBall()
    {
        return SpawnAtackSkill(20, SkillSpawner.fireBall);
    }
    public virtual bool IceShard()
    {
        return SpawnAtackSkill(20, SkillSpawner.iceShard);
    }
    public virtual bool Heal()
    {
        if (currentMana < 30) return false;
        playerCtrl.DamageReceiver.Health((playerCtrl.DamageReceiver.HPMax * 20) / 100);//Tính toán cho được 30% hp
        return SpawnEffectSkill(30, FXSpawner.heal);

    }
    public virtual bool Strength()
    {
        if (!PlayerCtrl.strengthOn)
        {
            return SpawnEffectSkill(0, FXSpawner.strength);
        }
        else
        {
            return false;
        }
    }
    public virtual bool Shield()
    {
        return SpawnEffectSkill(0, FXSpawner.shield);
    }

    public virtual void ManaRecover()
    {
        this.currentMana += 5 * Time.deltaTime;//Time.deltaTime
        if (this.currentMana > manaMax) currentMana = manaMax;
    }
    public virtual void ManaDeduct(float value)
    {
        this.currentMana -= value;
        if (currentMana < 0) currentMana = 0;
    }
    public virtual void ManaAdd(float value)
    {
        this.currentMana += value;
        if (currentMana > manaMax) currentMana = manaMax;
    }

    public virtual void SetManaMax(int maxMana)
    {
        this.manaMax = maxMana;
        if (this.currentMana >= manaMax) currentMana = manaMax;
    }
    public virtual void SetCurrentMana(float manaValue)
    {
        this.currentMana = (int)manaValue;
        if (this.currentMana >= manaMax) currentMana = manaMax;
    }
    private bool SpawnEffectSkill(int manaCost, string prefabName)
    {
        if (currentMana < manaCost)
        {
            Debug.Log("Can't use Effect Skill");
            return false;
        }
        ManaDeduct(manaCost);
        Vector3 position = transform.parent.position;
        Quaternion rotation = transform.parent.rotation;

        Transform newSkill = FXSpawner.Instance.Spawn(prefabName, position, rotation);
        if (newSkill == null) return false;
        newSkill.gameObject.SetActive(true);
        EffectSkillCtrl skillCtrl = newSkill.GetComponent<EffectSkillCtrl>();
        skillCtrl.SetPositionEF(transform);
        return true;
    }
    private bool SpawnAtackSkill(int manaCost, string prefabName)
    {
        if (currentMana < manaCost)
        {
            Debug.Log("Can't use Attack Skill");
            return false;
        }
        playerCtrl.PlayerSFX.SetSkillSFX();
        playerCtrl.PlaySound();
        ManaDeduct(manaCost);
        Vector3 position = transform.parent.position;
        Quaternion rotation = transform.parent.rotation;
        Transform newSkill = SkillSpawner.Instance.Spawn(prefabName, position, rotation);
        if (newSkill == null) return false;
        newSkill.gameObject.SetActive(true);
        AttackSkillCtrl skillCtrl = newSkill.GetComponent<AttackSkillCtrl>();
        skillCtrl.SetShooter(transform);
        return true;
    }
}
