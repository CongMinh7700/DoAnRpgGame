using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : RPGMonoBehaviour
{
    protected Animator animator;
    public Animator Animator => Animator;

    protected override void LoadComponents()
    {
        this.LoadAnimator();

    }
    public virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + "||LoadAnimator||", gameObject);

    }
    public virtual void IdlingAnimation(bool idling)
    {
        animator.SetBool("Idling", idling);
    }
    public virtual void FallAnimation(bool grounded)
    {
        animator.SetBool("Grounded", grounded);
    }
    public virtual void ShortSwordAnimation()
    {
        animator.SetTrigger("ShortSword");
    }
    public virtual void LongSwordAnimation()
    {
        animator.SetTrigger("LongSword");
    }
    public virtual void SpearAnimation()
    {
        animator.SetTrigger("Spear");
    }
    public virtual void LongAxeAnimation()
    {
        animator.SetTrigger("LongAxe");
    }
    public virtual void MagicAnimation()
    {
        animator.SetTrigger("Magic");
    }
    public virtual void AttackAnimation(string attackString)
    {
        animator.SetTrigger(attackString);
    }
}
