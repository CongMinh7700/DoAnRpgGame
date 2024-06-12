using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossMove : EnemyMove
{

    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private BossCtrl bossCtrl;
    [SerializeField] private float speedOffset;
    public bool isFlex = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossAttack();
        this.LoadBossCtrl();
    }
    public virtual void LoadBossAttack()
    {
        if (this.bossAttack != null) return;
        this.bossAttack = transform.parent.GetComponentInChildren<BossAttack>();
    }
    public virtual void LoadBossCtrl()
    {
        if (this.bossCtrl != null) return;
        this.bossCtrl = GetComponentInParent<BossCtrl>();
    }
    private void Start()
    {
        bossAttack.BossAnimation.Flex();
        isFlex = true;
        StartCoroutine(WaitToFalse());
    }
    public override void Attack()
    {
        if (bossCtrl.DamageReceiver.IsDead()) return;
        this.bossAttack.Attack();
        Vector3 pos = (player.transform.position - transform.position).normalized;
        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
    }
    public override void EnemyMovement()
    {
        IncreaseAttackRange();
        if (isFlex) return;

        base.EnemyMovement();
        if (BossAttack.canMove)
        {
            Vector3 pos = (player.transform.position - transform.parent.position).normalized;
            Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, posRotation, Time.deltaTime * (rotateSpeed/2));
            Debug.Log("Rotate");
            StartCoroutine(WaitToCantMove());
        }
        else
        {
            MoveToPlayer();
            return;
        }
    }
    public void IncreaseAttackRange()
    {
        if (!bossAttack.isPunch)
        {
            attackRange = 3f;
        }
        else
        {
            attackRange = 5f;
        }
        if (bossAttack.canIncreaseRange)
        {
            Debug.LogWarning("Call InCreaseRange");
            attackRange = 10f;
            StartCoroutine(WaitToDescreaseRange());
        }

    }
    public override void MoveToPlayer()
    {
        Debug.Log("NavMesh" + navMesh.isStopped);
        base.MoveToPlayer();

    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(2f);
        isFlex = false;
    }

    IEnumerator WaitToCantMove()
    {
        yield return new WaitForSeconds(2.1f);
        BossAttack.canMove = false;
    }
    IEnumerator WaitToDescreaseRange()
    {
        yield return new WaitForSeconds(2.1f);
        bossAttack.canIncreaseRange = false;
    }
}
