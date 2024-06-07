using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImpactOfPlayer : SkillImpact
{
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.skillCtrl.SkillDamageSender.Send(other.transform);
        }
    }
}
