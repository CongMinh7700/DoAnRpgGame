﻿using System.Collections;
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
        if (other.CompareTag("Enemy"))
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            UpdateIsOccupied();
        }
    }

    private void Update()
    {
        UpdateIsOccupied();
    }

    private void UpdateIsOccupied()
    {
        Vector3 center = this.boxCollider.bounds.center;
        Vector3 halfExtents = this.boxCollider.bounds.extents;
        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity);
        isOccupied = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                isOccupied = true;
                break;
            }
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
