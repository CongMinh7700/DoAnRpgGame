using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : EnemyAnimation
{
    public bool isDemon;
    public virtual void AttackCombo()
    {
        animator.SetTrigger("AttackCombo");
        if (isDemon)
        {
            StartCoroutine(WaitToShoot());
        }
     
    }
    public virtual void Flex()
    {
        animator.SetTrigger("Flex");
    }
    private bool SpawnFireBall()
    {
        Vector3 position = shooter.position;
        Quaternion rotation = shooter.rotation;
        Transform newSkill = SkillSpawner.Instance.Spawn(SkillSpawner.demonFireBall, position, rotation);
        if (newSkill == null) return false;
        newSkill.gameObject.SetActive(true);
        AttackSkillCtrl skillCtrl = newSkill.GetComponent<AttackSkillCtrl>();
        skillCtrl.SetShooter(shooter);
        return true;
    }
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(0.7f);
        SpawnFireBall();
        yield return new WaitForSeconds(0.7f);
        SpawnFireBall();
        yield return new WaitForSeconds(0.7f);
        SpawnFireBall();
    }
}
