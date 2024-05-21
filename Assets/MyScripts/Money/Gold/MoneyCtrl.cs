using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCtrl : RPGMonoBehaviour
{
    [SerializeField] protected DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance => despawnByDistance;
    [SerializeField] protected GoldPickup goldPickup;
    public GoldPickup GoldPickup => goldPickup;
    [SerializeField] protected Transform position;
    public Transform Position => position;
    protected override void LoadComponents()
    {
        LoadDespawn();
        LoadGoldPickUp();
    }
    protected virtual void LoadDespawn()
    {
        if (this.despawnByDistance != null) return;
        this.despawnByDistance = GetComponentInChildren<DespawnByDistance>();
    }
    protected virtual void LoadGoldPickUp()
    {
        if (this.goldPickup != null) return;
        this.goldPickup = GetComponentInChildren<GoldPickup>();
    }
    public virtual void SetPosition(Transform position)
    {
        this.position = position;
    }
}
