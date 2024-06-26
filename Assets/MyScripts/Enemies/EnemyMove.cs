﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : RPGMonoBehaviour
{

    [SerializeField] protected EnemyAnimation enemyAnimation;
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] public NavMeshAgent navMesh;

    [Header("Enemy Move")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected float x;
    [SerializeField] protected float z;
    [SerializeField] protected float velocitySpeed;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] protected float runRange = 50f;
    [SerializeField] protected float rotateSpeed = 50f;
    [SerializeField] private float distance;
    [SerializeField] private bool isAttacking = false;

    [Header("Enemy State Info")]
    private AnimatorStateInfo enemyInfo;
    public Vector3 originalPosition;
    protected override void OnEnable()
    {
        originalPosition = transform.parent.position;
    }
    protected override void OnDisable()
    {
        transform.parent.position = originalPosition;
        isAttacking = false;
    }
    protected override void LoadComponents()
    {
        this.LoadNavMeshAgent();
        this.LoadEnemyAnimation();
        this.LoadPlayer();
        this.LoadEnemyCtrl();
    }
          
    public virtual void LoadNavMeshAgent()
    {
        if (this.navMesh != null) return;
        this.navMesh = GetComponentInParent<NavMeshAgent>();
       // Debug.LogWarning(transform.name + "|LoadNavMeshAgent|", gameObject);
    }
    public virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponentInParent<EnemyCtrl>();
        //Debug.LogWarning(transform.name + "|LoadNavMeshAgent|", gameObject);
    }
    public virtual void LoadEnemyAnimation()
    {

        if (this.enemyAnimation != null) return;
        this.enemyAnimation = GetComponentInParent<EnemyAnimation>();
       // Debug.LogWarning(transform.name + "|LoadEnemyAnimation|", gameObject);
    }
    public virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
        //Debug.LogWarning(transform.name + "|LoadPlayer|", gameObject);
    }
    private void Start()
    {
        navMesh.avoidancePriority = Random.Range(5, 75);
    }
    private void Update()
    {

        this.EnemyMovement();
    }
    public virtual void EnemyMovement()
    {
      
        x = navMesh.velocity.x;
        z = navMesh.velocity.z;
        velocitySpeed = x + z;

        if (velocitySpeed == 0)
        {
            this.enemyAnimation.WalkAnimation(false);
        }
        else
        {
            this.enemyAnimation.WalkAnimation(true);
            isAttacking = false;
        }

        enemyInfo = enemyAnimation.Animator.GetCurrentAnimatorStateInfo(0);
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < attackRange)
        {
            navMesh.isStopped = true;
            //Debug.LogWarning("Current State Tag: " + enemyInfo.tagHash);
            if (enemyInfo.IsTag("NonAttack") && !enemyAnimation.Animator.IsInTransition(0))//trạng thái chuyển tiếp
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    this.Attack();
                }
            }

            if (enemyInfo.IsTag("Attack"))
            {
                this.StopAttack();
            }
        }
        else if (distance <= runRange)
        {
            navMesh.isStopped = false;
            this.MoveToPlayer();
        }
        else
        {
            float distanceToOriginalPosition = Vector3.Distance(transform.position, originalPosition);
            if (distanceToOriginalPosition > 1f) // Giới hạn khoảng cách nhỏ để dừng tại vị trí gốc
            {
                navMesh.isStopped = false;
                MoveToPosition(originalPosition);
            }
            else
            {
                navMesh.isStopped = true;
                this.enemyAnimation.WalkAnimation(false);
            }
        }
       
      //  Debug.Log("Navemesh Speed" + navMesh.speed);
    }
    public virtual void Attack()
    {
        this.enemyAnimation.AttackAnimation();

    }
    public virtual void MoveToPlayer()
    {
        if (enemyCtrl.DamageReceiver.IsDead())
        {
            navMesh.isStopped = true;
            return;
        }
        navMesh.isStopped = false;
        navMesh.destination = player.transform.position;
    }
    public virtual void MoveToPosition(Vector3 targetPosition)
    {
        navMesh.destination = targetPosition;
    }
    public virtual void StopAttack()
    {
        if (isAttacking) isAttacking = false;
    }
 

}
