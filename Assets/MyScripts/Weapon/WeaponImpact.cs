using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WeaponImpact : RPGMonoBehaviour
{
    [Header("Weapon Impact")]
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected WeaponCtrl weaponCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
        this.LoadRigidBody();
        this.LoadWeaponCtrl();
    }
    public virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        this.boxCollider.isTrigger = true;
        Debug.LogWarning(transform.name + "|LoadBoxCollider|", gameObject);
    }
    public virtual void LoadRigidBody()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody>();
        Debug.LogWarning(transform.name + "|LoadRigidBody|", gameObject);
    }
    public virtual void LoadWeaponCtrl()
    {
        if (this.weaponCtrl != null) return;
        this.weaponCtrl = GetComponentInParent<WeaponCtrl>();
        Debug.LogWarning(transform.name + "|LoadWeaponCtrl|", gameObject);
    }
    
}
