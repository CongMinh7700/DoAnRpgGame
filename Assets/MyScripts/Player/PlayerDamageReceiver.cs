using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : HitableObjectDamageReceiver
{
    private void Update()
    {
        UpdateHpMax();
    }
    void UpdateHpMax()
    {
        if (ItemManager.isEquippedHelmet == false)
        {
            SetHpMax(hitableObjectCtrl.HitableObjectSO.hpMax);
            SetDefense(hitableObjectCtrl.HitableObjectSO.defense);
        }
        else
        {
            SetHpMax(hitableObjectCtrl.HitableObjectSO.hpMax + ItemManager.hpMaxBonus);
            SetDefense(hitableObjectCtrl.HitableObjectSO.defense+ItemManager.bonusDefense);
        }
    }
    protected override void OnDead()
    {
        Debug.Log("Bạn đã chết");
    }
}
