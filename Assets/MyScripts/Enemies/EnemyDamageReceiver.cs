﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : HitableObjectDamageReceiver
{
    [SerializeField] protected EnemyAnimation enemyAnimation;

    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected int maxExp;
    [SerializeField] protected int minExp;
    [SerializeField] protected int exp;


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
        exp = Random.Range(minExp, maxExp);
        Debug.Log(exp);
        enemyAnimation.DeathAnimation();
        //Money & Xp
        LevelSystem.Instance.GainExperienceFlatRate(exp);
        SpawnMoney();
        Debug.LogWarning(transform.name + "đã chết");
        string enemyName = enemyCtrl.GetEnemyName();
        QuestManager.Instance.UpdateQuestProgress(enemyName);
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.SetActive(false);
    }

    private void SpawnMoney()
    {
        Vector3 position = transform.parent.position;
        Quaternion rotation = transform.parent.rotation;

        Transform newGold = GoldSpawner.Instance.Spawn(GoldSpawner.gold, position, rotation);
        newGold.gameObject.SetActive(true);
        MoneyCtrl moneyCtrl = newGold.GetComponent<MoneyCtrl>();
        moneyCtrl.GoldPickup.SetMoney(exp);
        moneyCtrl.SetPosition(transform);
    }


}
