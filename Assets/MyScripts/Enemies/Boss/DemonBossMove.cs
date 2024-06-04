using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossMove : EnemyMove
{

    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private float speedOffset;
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
    }
    private void Start()
    {
       bossAttack.BossAnimation.Flex();
       isFlex = true;
        StartCoroutine(WaitToFalse());
    }
    public override void Attack()
    {
        this.bossAttack.Attack();
        Vector3 pos = (player.transform.position - transform.position).normalized;
        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
    }
    public override void EnemyMovement()
    {
        if (isFlex) return;
        base.EnemyMovement();
        IncreaseAttackRange();
        if (BossAttack.canMove)
        {
            MoveToPlayer();
            StartCoroutine(WaitToMove());
        }
        else
        {
            return;
        }

    }
    public void IncreaseAttackRange()
    {
        if (!bossAttack.isPunch)
        {
            attackRange = 5f;
        }
        else if (bossAttack.canIncreaseRange)
        {
            Debug.LogWarning("Call InCreaseRange");
            attackRange = 10f;
            bossAttack.canIncreaseRange = false;
        }
        else
        {
            attackRange = 3f;
        }
    }
    public override void MoveToPlayer()
    {
        Debug.Log("NavMesh" + navMesh.isStopped);
        if (bossAttack.isCombo)
        {
            attackRange = 10f;
            BossAttack.canMove = false;

        }
        else
        {
            base.MoveToPlayer();
        }

    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(2f);
        isFlex = false;
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(2.1f);
        BossAttack.canMove = false;
    }
}
