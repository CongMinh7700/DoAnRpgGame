using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WImpactForEnemy : WeaponImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerCtrl.shieldOn)
        {
            this.weaponCtrl.WeaponDamageSender.Send(other.transform);
            CreateHitEffect();
            SFXManager.Instance.PlaySFXImpact();
        }

    }
    protected virtual void CreateHitEffect()
    {
        string fxName = FXSpawner.hitEffect;
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        fxObj.gameObject.SetActive(true);
    }

}
