using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class GoldPickup : RPGMonoBehaviour
{
    [SerializeField] protected MoneyCtrl moneyCtrl;
    public MoneyCtrl MoneyCtrl => moneyCtrl;
    [SerializeField] protected SphereCollider sphereCollider;
    public int money;
    protected override void LoadComponents()
    {
        LoadMoneyCtrl();
        LoadSphereCollider();
        
    }
    protected virtual void LoadMoneyCtrl()
    {
        if (this.moneyCtrl != null) return;
        this.moneyCtrl = GetComponentInParent<MoneyCtrl>();

    }
    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySFXPickUp();
            Picked();
        }
    }
    public virtual void Picked()
    {
        MoneyManager.Instance.AddGold(money);
        moneyCtrl.DespawnByDistance.DespawnObject();
        GoldSpawner.Instance.Despawn(transform.parent);
    }
  
    public void SetMoney(int value)
    {
        money = value;
    }
}
