using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageSender : DamageSender
{
    [SerializeField] protected WeaponCtrl weaponCtrl;
    [SerializeField] protected HitableObjectCtrl hitableObjectCtrl;
    public HitableObjectCtrl HitableObjectCtrl => hitableObjectCtrl;
    //SetDamage
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponCtrl();
        LoadHitableObjectCtrl();
    }
    //private void Start()
    //{
    //    if(hitableObjectCtrl!= null) this.damage = hitableObjectCtrl.HitableObjectSO.damage;

    //}
    public virtual void LoadWeaponCtrl()
    {
        if (this.weaponCtrl != null) return;
        this.weaponCtrl = GetComponentInParent<WeaponCtrl>();
        Debug.LogWarning(transform.name + "|LoadWeaponCtrl|", gameObject);
    }
    public virtual void LoadHitableObjectCtrl()
    {
        Transform currentTransform = transform;

        // Duyệt qua các cấp cha cho đến khi tìm thấy HitableObjectCtrl hoặc không còn cha nào nữa
        while (currentTransform != null)
        {
            hitableObjectCtrl = currentTransform.GetComponent<HitableObjectCtrl>();
            if (hitableObjectCtrl != null)
            {
                this.damage = hitableObjectCtrl.HitableObjectSO.damage;
                break; // Nếu tìm thấy thì thoát khỏi vòng lặp
            }
            currentTransform = currentTransform.parent; // Đi lên cấp cha tiếp theo
        }

        if (hitableObjectCtrl == null)
        {
            Debug.LogWarning("HitableObjectCtrl not found in parent hierarchy.");
        }
    }
    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);

    }

}
