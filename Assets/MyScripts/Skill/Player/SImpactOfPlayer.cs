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
        yield return new WaitForSeconds(0.8f);
        sphereCollider.enabled = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereCollider.radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                this.skillCtrl.SkillDamageSender.Send(hitCollider.transform);
            }
        }
    }
}
