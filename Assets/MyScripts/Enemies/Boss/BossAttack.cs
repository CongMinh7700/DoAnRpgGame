using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : EnemyAttack
{
    [SerializeField] protected BossAnimation bossAnimation;
    public BossAnimation BossAnimation => bossAnimation;
    [SerializeField] private int attackCount = 0;
    [SerializeField] private int countLimit = 5;
    [SerializeField] public static bool canMove ;
    [SerializeField] public  bool isPunch ;
    [SerializeField] public  bool canIncreaseRange ;
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
    private void Start()
    {
        
    }

    private void Update()
    {
        if (attackCount >= countLimit)
        {
            canIncreaseRange = true;
        }
    }
    public void Attack()
    {
        canMove = false;
        isPunch = Random.value > 0.2f;
        bossAnimation.Animator.SetBool("IsPunch", isPunch);
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
        yield return new WaitForSeconds(1f);
        attackCount = 0;
       // canIncreaseRange = false;
    } 
  
    //5
    //warrok 1.25
    IEnumerator ResetSpeed()
    {
        
        yield return new WaitForSeconds(2f);
  
        isCombo = false;
       

    }

}
