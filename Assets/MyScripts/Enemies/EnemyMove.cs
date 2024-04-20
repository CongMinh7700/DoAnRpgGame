using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : RPGMonoBehaviour
{
    [SerializeField] protected EnemyAnimation enemyAnimation;
    [SerializeField] protected NavMeshAgent navMesh;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float x;
    [SerializeField] protected float z;
    [SerializeField] protected float velocitySpeed;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] protected float runRange = 50f;
    [SerializeField] protected float rotateSpeed = 50f;


    private AnimatorStateInfo enemyInfo;
    private float distance;
    private bool isAttacking = false;

    protected override void LoadComponents()
    {
        this.LoadNavMeshAgent();
        this.LoadEnemyAnimation();
        this.LoadPlayer();
    }
    public virtual void LoadNavMeshAgent()
    {
        if (this.navMesh != null) return;
        this.navMesh = GetComponentInParent<NavMeshAgent>();
        Debug.LogWarning(transform.name + "|LoadNavMeshAgent|", gameObject);
    }
    public virtual void LoadEnemyAnimation()
    {

        if (this.enemyAnimation != null) return;
        this.enemyAnimation = GetComponentInParent<EnemyAnimation>();
        Debug.LogWarning(transform.name + "|LoadEnemyAnimation|", gameObject);
    }
    public virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
        Debug.LogWarning(transform.name + "|LoadPlayer|", gameObject);
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

        if (distance < attackRange || distance > runRange)
        {
            navMesh.isStopped = true;
            //if(distance > runRange)
            //{
            //    //Destroy(gameObject);
            //}
            //Kiểm tra trạng thái và có đang chuyển đổi trạng thái hay không
            if (distance < attackRange && enemyInfo.IsTag("NonAttack") && !enemyAnimation.Animator.IsInTransition(0))
            {
                if (!isAttacking)
                {
                    isAttacking = true;//
                    this.Attack();
                }
            }
            if (distance < attackRange && enemyInfo.IsTag("Attack"))
            {
                this.StopAttack();

            }
        }
        else if (distance > attackRange && enemyInfo.IsTag("NonAttack") && !enemyAnimation.Animator.IsInTransition(0))
        {
            this.MoveToPlayer();
        }
    }
    public virtual void Attack()
    {
        this.enemyAnimation.AttackAnimation();
        Vector3 pos = (player.transform.position - transform.position).normalized;
        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
    }
    public virtual void MoveToPlayer()
    {
        navMesh.isStopped = false;
        navMesh.destination = player.transform.position;
    }
    public virtual void StopAttack()
    {
        if (isAttacking) isAttacking = false;
    }
}
