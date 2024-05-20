using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDamageSender : WeaponDamageSender
{
    private void Update()
    {
        Debug.Log("Strength On :" + PlayerCtrl.strengthOn);
        //SetDoublDamage neu bat cuong no
        if ( PlayerCtrl.strengthOn)
        {
            SetDamage((hitableObjectCtrl.HitableObjectSO.damage + ItemManager.bonusAttack) * 2);
        }
        else
        {
            SetDamage(hitableObjectCtrl.HitableObjectSO.damage + ItemManager.bonusAttack);
        }


    }
}
