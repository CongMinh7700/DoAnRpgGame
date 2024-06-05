using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDamageReceiver : HitableObjectDamageReceiver
{
    [SerializeField] protected EnemyAnimation enemyAnimation;

    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected int maxExp;
    [SerializeField] protected int minExp;
    [SerializeField] protected int exp;
    [SerializeField] GameObject thisEnemy;
    [SerializeField] protected Image healthBar;
   
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyAnimation();
        this.LoadEnemyCtrl();
        LoadThisEnemy();
    }


    protected virtual void LoadThisEnemy()
    {
        if (thisEnemy != null) return;
        thisEnemy = gameObject;
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponentInParent<EnemyCtrl>();
    }
    private void Update()
    {
        OutlineControl();
        SetHpUI();
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
       // Debug.LogWarning(transform.name + "|LoadEnemyAnimation|", gameObject);
    }
    protected override void OnDead()
    {
        exp = Random.Range(minExp, maxExp);
        Debug.Log(exp);
        enemyAnimation.DeathAnimation();
        //Money & Xp
        PlayerCtrl.theTarget = null;
        CreateDeathEffect();
        LevelSystem.Instance.GainExperienceFlatRate(exp);
        SpawnMoney();
        Debug.LogWarning(transform.name + "đã chết");
        string enemyName = enemyCtrl.GetEnemyName();
        QuestManager.Instance.UpdateQuestProgress(enemyName);
        if(hitableObjectCtrl.HitableObjectSO.objType == ObjectType.Boss)
        {
            LevelSystem.Instance.BossKilled();
        }
    }

    
    public virtual void OutlineControl()
    {
  
        if (!IsDead())
        {
           
            if (PlayerCtrl.theTarget == thisEnemy)
            {
                transform.parent.GetComponent<Outline>().enabled = true;
          
            }
            else
            {
                transform.parent.GetComponent<Outline>().enabled = false;
               
            }
        }
    }
    protected virtual void SetHpUI()
    {
        this.healthBar.fillAmount = (float)currentHp / hpMax;
       // Debug.Log("Call SetHPUI");
    }
    //Spawn
    private void SpawnMoney()
    {
        Vector3 position = transform.parent.position;
        Quaternion rotation = transform.parent.rotation;

        Transform newGold = GoldSpawner.Instance.Spawn(GoldSpawner.gold, position, rotation);
        newGold.gameObject.SetActive(true);
        MoneyCtrl moneyCtrl = newGold.GetComponent<MoneyCtrl>();
        moneyCtrl.GoldPickup.SetMoney((int)exp/10);
        moneyCtrl.SetPosition(transform);
    }
    protected virtual void CreateDeathEffect()
    {
        string fxName = FXSpawner.deathEffect;

        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        fxObj.gameObject.SetActive(true);
    }
}
