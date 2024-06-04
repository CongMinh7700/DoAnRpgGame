using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImpactOfEnemy : SkillImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.skillCtrl.SkillDamageSender.Send(other.transform);
        }
    }
}
