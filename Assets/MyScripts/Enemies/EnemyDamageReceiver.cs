using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : HitableObjectDamageReceiver
{
    
    [SerializeField] protected EnemyAnimation enemyAnimation;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyAnimation();
    }

    private void Update()
    {
        if (isAttacked)
        {
            isAttacked = false;
            enemyAnimation.HitAnimation();
        }
    }

    public virtual void LoadEnemyAnimation()
    {
        if (this.enemyAnimation != null) return;
        this.enemyAnimation = GetComponentInParent<EnemyAnimation>();
        Debug.LogWarning(transform.name + "|LoadEnemyAnimation|", gameObject);
    }
    protected override void OnDead()
    {
        enemyAnimation.DeathAnimation();
        Debug.LogWarning(transform.name + "đã chết");
        //Despawn
 
           
        
     
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.SetActive(false);
    }
    //Fill Bar cho thanh mau
    //Set Outline cho enemy


}
