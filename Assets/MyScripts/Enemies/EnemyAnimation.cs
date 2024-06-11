using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : RPGMonoBehaviour
{
 
    protected  Animator animator;
    protected  Transform player;
    public  Animator Animator => animator;
    public Transform shooter;
    public bool magicEnemy;
    protected override void LoadComponents()
    {
        this.LoadEnemyAnimator();
        LoadPlayerTarget();
    }
    public  virtual void LoadEnemyAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        //Debug.LogWarning(transform.name + "||LoadEnemyAnimator||", gameObject);

    }
    public virtual void LoadPlayerTarget()
    {
        if (this.player != null) return;
        this.player = FindObjectOfType<PlayerCtrl>().transform;
        //Debug.LogWarning(transform.name + "||LoadEnemyAnimator||", gameObject);

    }
    public virtual void WalkAnimation(bool run)
    {
        animator.SetBool("Run",run);
    }
    public virtual void AttackAnimation()
    {
        animator.SetTrigger("Attack");
        if (magicEnemy)
        {
          //  transform.LookAt(player.transform);
            SpawnFireBall();
        }
        transform.LookAt(player.transform);//cứu tinh cho sự chuẩn sát :)))
    }
    public virtual void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }
    public virtual void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }


    private bool SpawnFireBall()
    {
        Debug.LogWarning("Spawn Enemy Fire");
        Vector3 position = shooter.position;
        Quaternion rotation = shooter.rotation;
        Transform newSkill = SkillSpawner.Instance.Spawn(SkillSpawner.enemyFireBall, position, rotation);
        if (newSkill == null) return false;
        newSkill.gameObject.SetActive(true);
        AttackSkillCtrl skillCtrl = newSkill.GetComponent<AttackSkillCtrl>();
        skillCtrl.SetShooter(shooter);
        return true;
    }
}
