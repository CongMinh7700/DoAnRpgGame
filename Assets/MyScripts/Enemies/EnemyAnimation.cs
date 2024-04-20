using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : RPGMonoBehaviour
{
    protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        this.LoadEnemyAnimator();
    }
    public  virtual void LoadEnemyAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + "||LoadEnemyAnimator||", gameObject);

    }
    public virtual void WalkAnimation(bool run)
    {
        animator.SetBool("Run",run);
    }
    public virtual void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public virtual void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }
    public virtual void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}
