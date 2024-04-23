using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private float speedOffset;



    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossAttack();
    }
    public virtual void LoadBossAttack()
    {
        if (this.bossAttack != null) return;
        this.bossAttack = transform.parent.GetComponentInChildren<BossAttack>();
        Debug.LogWarning(transform.name + "||LoadBossAttack||", gameObject);
    }
    public override void Attack()
    {
        this.bossAttack.Attack();
        Vector3 pos = (player.transform.position - transform.position).normalized;
        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
    }
    public override void MoveToPlayer()
    {
        Debug.Log("NavMesh" + navMesh.isStopped);
        if (bossAttack.isCombo)
        {
            navMesh.speed = 1.75f + speedOffset;
           
        }
        else
        {
            navMesh.speed = 3.5f;
           
        }
         
        //Warrok =5
        if (bossAttack.canIncreaseRange)
        {
            attackRange = 20f;
        }
        else
        {
            attackRange = 3f;
        }
        //  Debug.Log("AttackRange :" + attackRange);
        base.MoveToPlayer();

        //warrok
        //if (bossAttack.isCombo)
        //{
          
        //    StartCoroutine(WaitToMove());
        //}
       
    }
    //warrok
    IEnumerator WaitToMove()
    {

        yield return new WaitForSeconds(2.17f);
        navMesh.isStopped = true;
    }
}


