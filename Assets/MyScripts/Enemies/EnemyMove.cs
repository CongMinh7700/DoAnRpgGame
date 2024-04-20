using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : RPGMonoBehaviour
{
    [SerializeField] protected EnemyAnimation enemyAnimation;
    [SerializeField] protected  NavMeshAgent navMesh;
    [SerializeField] protected GameObject player;
    [SerializeField] protected  float x;
    [SerializeField] protected  float z;
    [SerializeField] protected  float velocitySpeed;
    [SerializeField] protected  float attackRange;
    [SerializeField] protected  float runRange;
    [SerializeField] protected  float rotateSpeed;


    private AnimatorStateInfo enemyInfo;
    private float distance;
    private bool isAttacking = false;
    private WaitForSeconds lookTime = new WaitForSeconds(2f);

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
        this.attackRange = 2f;
        this.runRange = 50f;
        this.rotateSpeed = 50f;
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
       
        if(velocitySpeed == 0)
        {
            this.enemyAnimation.WalkAnimation(false);
        }
        else
        {
            this.enemyAnimation.WalkAnimation(true);
            //
            isAttacking = false;
        }
        enemyInfo = enemyAnimation.Animator.GetCurrentAnimatorStateInfo(0);

        distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Velocity Speed : " + distance);
        if (distance < attackRange || distance > runRange)
        {
            navMesh.isStopped = true;
            if(distance > runRange)
            {
                //Destroy(gameObject);
            }
            if(distance < attackRange && enemyInfo.IsTag("NonAttack") && !enemyAnimation.Animator.IsInTransition(0))
            {
                if (!isAttacking)
                {
                    isAttacking = true;//
                    this.enemyAnimation.AttackAnimation();
                    //transform.position => 
                    Vector3 pos = (player.transform.position - transform.position).normalized;
                    Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
                }
            }
            if(distance < attackRange && enemyInfo.IsTag("Attack"))
            {
                if (isAttacking) isAttacking = false;
                
            }
        }else if( distance > attackRange && enemyInfo.IsTag("NonAttack" ) && !enemyAnimation.Animator.IsInTransition(0))
        {
            navMesh.destination = player.transform.position;
            navMesh.isStopped = false;
            //if (SaveScripts.invisible == false)
            //{
            //   
            //    
            //}
        }
    }
}
