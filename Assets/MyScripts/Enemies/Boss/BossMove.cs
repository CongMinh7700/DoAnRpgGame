using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private Transform model;
    public static bool canAttack = true;
    private WaitForSeconds delayTime = new WaitForSeconds(2f);
  
    public virtual void LoadBossAttack()
    {
        if (this.bossAttack != null) return;
        this.bossAttack = transform.parent.GetComponent<BossAttack>();
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
        if (bossAttack.isCombo)
        {
            navMesh.speed = 1.75f;
        }
        else
        {
            navMesh.speed = 3.5f;
        }
        base.MoveToPlayer();

    }
   
    
}

