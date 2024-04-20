using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WImpactForEnemy : WeaponImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.weaponCtrl.WeaponDamageSender.Send(other.transform);

        }

    }
  
}
