using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : RPGMonoBehaviour
{
    protected Animator animator;
    public Animator Animator => Animator;

    private GameObject trailObject;
    private WaitForSeconds trailOffTime = new WaitForSeconds(0.1f);
   

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
        Debug.Log(transform.name +"|LoadTrail|",gameObject);
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

    public void TrailOn()
    {
        trailObject.GetComponent<Renderer>().enabled = true;
    }
    public void TrailOff()
    {
        trailObject.GetComponent<Renderer>().enabled = false;

    }
    //Sử dụng cho đổi vũ khí
    IEnumerator TurnOffTrail()
    {
        yield return trailOffTime;
        trailObject = GameObject.Find("Trail");
        trailObject.GetComponent<Renderer>().enabled = false;
    }
}
