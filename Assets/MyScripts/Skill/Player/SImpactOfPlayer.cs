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
    protected override void OnEnable()
    {
        if (transform.parent.name == "DimenBoom")
        {
            sphereCollider.enabled = false;
            StartCoroutine(WaitTurnOn());
        }
    }
    IEnumerator WaitTurnOn()
    {
        yield return new WaitForSeconds(1f);
        sphereCollider.enabled = true;
    }
}
