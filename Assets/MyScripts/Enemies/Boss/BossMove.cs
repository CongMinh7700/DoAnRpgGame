using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private float speedOffset;
    public bool warrok = false;
    public bool isFlex = false;
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
    private void Start()
    {
        if (warrok)
        {
            bossAttack.BossAnimation.Flex();
            isFlex = true;
            StartCoroutine(WaitToFalse());
        }
    }
    public override void Attack()
    {
        this.bossAttack.Attack();
        //
        Vector3 pos = (player.transform.position - transform.position).normalized;
        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
    }
    public override void EnemyMovement()
    {
        if (isFlex) return;
        base.EnemyMovement();
        //Mấu chốt là ở đây
        if (BossAttack.canMove)
        {
            MoveToPlayer();
           //BossAttack.canMove = false;
        }
    }
    public override void MoveToPlayer()
    {
        //Debug.Log("NavMesh" + navMesh.isStopped);
        IncreaseAttackRange();
        if (bossAttack.isCombo && warrok)
        {
            StartCoroutine(WaitToIncreaseSpeed());
           
        }
        else
        {
            navMesh.speed = 3.5f;
           
        }

        //  Debug.Log("AttackRange :" + attackRange);
        base.MoveToPlayer();

        //warrok
        if (bossAttack.isCombo && warrok)
        {

            StartCoroutine(WaitToMove());
        }

    }
    public virtual void IncreaseAttackRange()
    {
        if (bossAttack.canIncreaseRange && warrok)
        {
            attackRange = 20f;
            bossAttack.canIncreaseRange = false;
        }
        else
        {

            if (warrok) attackRange = 5f;
            else attackRange = 3f;
        }
    }
    //warrok
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(2.17f);
        navMesh.isStopped = true;
    }
    IEnumerator WaitToIncreaseSpeed()
    {
        yield return new WaitForSeconds(0.7f);
        navMesh.speed = 1.75f + speedOffset;
    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(4.67f);
        isFlex = false;
    }
}


