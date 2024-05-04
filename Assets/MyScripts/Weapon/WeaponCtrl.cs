using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : RPGMonoBehaviour
{
    [SerializeField] protected WeaponDamageSender weaponDamageSender;
    public WeaponDamageSender WeaponDamageSender => weaponDamageSender;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
        this.LoadPlayerCtrl();
    }
    public virtual void LoadDamageSender()
    {
        if (this.weaponDamageSender != null) return;
        this.weaponDamageSender = GetComponentInChildren<WeaponDamageSender>();
        Debug.LogWarning(transform.name + "|LoadDamageSender|", gameObject);
      
    }
    public virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.root.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + "|LoadHitableObjectCtrl|", gameObject);
    }
  
}
