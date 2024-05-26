using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class PointTrigger : RPGMonoBehaviour
{
    [Header("Point Trigger")]
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected Rigidbody rbBody;
    [SerializeField] protected bool isOccupied = false;
    protected override void LoadComponents()
    {
        LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        this.boxCollider.isTrigger = true;
    }
    //protected virtual void LoadRigidBody()
    //{
    //    if (this.rbBody != null) return;
    //    this.rbBody = GetComponent<Rigidbody>();
    //    this.rbBody.useGravity = false;
    //    this.rbBody.
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            isOccupied = true;
            Debug.Log("ISOccupied");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isOccupied = false;
        }
    }
    public  bool IsOccupied()
    {
        return isOccupied == true;
    }
}
