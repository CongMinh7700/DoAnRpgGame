using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : HitableObjectDamageReceiver
{
    
    [SerializeField] protected EnemyAnimation enemyAnimation;

    [SerializeField] protected EnemyCtrl enemyCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyAnimation();
        this.LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponentInParent<EnemyCtrl>();
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
        string enemyName = enemyCtrl.GetEnemyName();
        Debug.Log(enemyName);
        QuestManager.Instance.OnEnemyKilled(enemyName);
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.SetActive(false);
    }



}
