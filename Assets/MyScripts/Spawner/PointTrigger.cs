using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PointTrigger : RPGMonoBehaviour
{
    [Header("Point Trigger")]
    [SerializeField] protected BoxCollider boxCollider;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            isOccupied = true;
          //  Debug.Log("IsOccupied");
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
