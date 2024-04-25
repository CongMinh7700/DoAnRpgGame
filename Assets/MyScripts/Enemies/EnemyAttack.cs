using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : RPGMonoBehaviour
{

    [SerializeField] private EnemyAnimation enemyAnimation;
    protected override void LoadComponents()
    {
        this.LoadEnemyAnimation();

    }
    public virtual void LoadEnemyAnimation()
    {
        if (this.enemyAnimation != null) return;
        this.enemyAnimation = GetComponentInParent<EnemyAnimation>();
        Debug.Log(transform.name + "||LoadEnemyAnimation", gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
