using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WImpactForPlayer : WeaponImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.weaponCtrl.WeaponDamageSender.Send(other.transform);        
        }
       
    }
}
