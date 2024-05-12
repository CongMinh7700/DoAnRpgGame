using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SkillImpact : SkillAbstract
{
    [Header("Bullet Impact")]
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected Rigidbody rgbody;



    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();
        this.LoadRigidBody();
    }
    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 0.25f;
        this.sphereCollider.isTrigger = true;
        Debug.LogWarning(transform.name + "||LoadSphereCollider||", gameObject);
    }
    protected virtual void LoadRigidBody()
    {
        if (this.rgbody != null) return;
        this.rgbody = GetComponent<Rigidbody>();
        this.rgbody.isKinematic = true;
        Debug.LogWarning(transform.name + "||LoadRigidBody||", gameObject);
    }

}
