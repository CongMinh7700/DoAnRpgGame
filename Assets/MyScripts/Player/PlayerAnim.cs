using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : RPGMonoBehaviour
{
    protected Animator animator;
    public Animator Animator => animator;

    private GameObject trailObject;
    public bool isAttacking;

    protected override void LoadComponents()
    {
        this.LoadAnimator();

    }
    public virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        //Debug.LogWarning(transform.name + "||LoadAnimator||", gameObject);

    }
    public virtual void LoadTrail()
    {
        if (this.trailObject != null) return;
        this.trailObject = GameObject.Find("Trail");
        TrailOff();
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
        string newName = RenameAnimation(attackString);
        if (newName == "") return;
        animator.SetTrigger(newName);
        isAttacking = true;
    }
    public string RenameAnimation(string name)
    {
        if (name == "Spear" || name == "WSpear")
        {
            name = "Spear";
        }
        else if (name == "LongSword" || name == "WSword")
        {
            name = "LongSword";
        }
        else if (name == "LongAxe" || name == "WAxe")
        {
            name = "LongAxe";
        }
        else name = "";
        return name;
    }
    public void TrailOn()
    {
        if (trailObject == null) return;
        trailObject.GetComponent<Renderer>().enabled = true;
    }
    public void TrailOff()
    {
        if (trailObject == null) return;
        trailObject.GetComponent<Renderer>().enabled = false;

    }
    public bool IsPlayingAttackAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isPlayingAttack = stateInfo.IsTag("Attack") && stateInfo.normalizedTime < 1f;//o là bắt đầu ,1 là kết thúc trạng thái đang pháts
        return isPlayingAttack;

    }
}
