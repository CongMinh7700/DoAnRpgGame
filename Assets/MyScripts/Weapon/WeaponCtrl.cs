using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : RPGMonoBehaviour
{
    [SerializeField] protected WeaponDamageSender weaponDamageSender;
    public WeaponDamageSender WeaponDamageSender => weaponDamageSender;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
    }
    public virtual void LoadDamageSender()
    {
        if (this.weaponDamageSender != null) return;
        this.weaponDamageSender = GetComponentInChildren<WeaponDamageSender>();
        Debug.LogWarning(transform.name + "|LoadDamageSender|", gameObject);
      
    }
}
