using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageSender : DamageSender
{
    [SerializeField] protected AttackSkillCtrl skillCtrl;
    public bool isFire = false;
    public bool isDimenBoom = false;
    
    //true,false hoặc lấy attribute
    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
    }
    protected override void OnEnable()
    {
        if (isFire)
            SetDamage(LevelSystem.damageLevel * 2);
        else if (isDimenBoom)
            SetDamage(LevelSystem.damageLevel * 4);
        else SetDamage(LevelSystem.damageLevel * 3);
    }
    protected virtual void LoadSkillCtrl()
    {
        if (this.skillCtrl != null) return;
        this.skillCtrl = GetComponentInParent<AttackSkillCtrl>();
    }

    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        Vector3 hitPos = transform.position;
        CreateHitEffect();
        if (isFire) SFXManager.Instance.PlayFireImpact();
        else SFXManager.Instance.PlayIceImpact();
        if (!isDimenBoom) this.DestroySkill();
        else StartCoroutine(WaitToDespawn());
        
    }
    protected virtual void DestroySkill()
    {
        this.skillCtrl.SkillDespawn.DespawnObject();
    }
    protected virtual void CreateHitEffect()
    {
        string fxName = "";
        if (isFire)
        {
             fxName = FXSpawner.fireHitEffect;
        }else if (isDimenBoom)
        {
            fxName = FXSpawner.dEffect;
        }
        else
        {
            fxName = FXSpawner.iceHitEffect;
        }
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        fxObj.gameObject.SetActive(true);
    }
    IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(2f);
        this.DestroySkill();
    }
}
