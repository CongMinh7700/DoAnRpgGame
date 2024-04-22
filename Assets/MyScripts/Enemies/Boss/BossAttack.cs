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
      
        bossAnimation.AttackAnimation();
        attackCount++;
        if (attackCount >= countLimit)
        {
            canMove = true;
            attackCount = 0;
            bossAnimation.AttackCombo();
        }
       

    }
}
