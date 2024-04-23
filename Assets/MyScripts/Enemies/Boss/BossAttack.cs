using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : EnemyAttack
{
    [SerializeField] protected BossAnimation bossAnimation;

    [SerializeField] private int attackCount = 0;
    [SerializeField] private int countLimit = 5;
    [SerializeField] public static bool canMove ;
    public  bool isCombo;
    protected override void LoadComponents()
    {
     
        this.LoadBossAnimation();

        
       
    }
    public virtual void LoadBossAnimation()
    {

        if (this.bossAnimation != null) return;
        this.bossAnimation = GetComponentInParent<BossAnimation>();
        Debug.LogWarning(transform.name + "|LoadBossAnimation|", gameObject);
    }
   

    public void Attack()
    {
        canMove = false;
        if (attackCount < countLimit)
        {      
            bossAnimation.AttackAnimation();

        }
            
        if (attackCount >= countLimit)
        {
            isCombo = true;
            canMove = true;
            bossAnimation.AttackCombo();
            StartCoroutine(ResetAttackCount());
            StartCoroutine(ResetSpeed());
        }
        attackCount++;

    }
    IEnumerator ResetAttackCount()
    {
        yield return new WaitForSeconds(1.37f);
        attackCount = 0;
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(5f);
        isCombo = false;
    }

}
