using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMoveToEnemy : RPGMonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected SkillFly skillFly;
    protected override void LoadComponents()
    {
        LoadSkillFly();
    }
    protected virtual void LoadSkillFly()
    {
        if (this.skillFly != null) return;
        this.skillFly = GetComponentInChildren<SkillFly>();
    }
    protected override void OnEnable()
    {
        if (PlayerCtrl.theTarget != null)
            target = PlayerCtrl.theTarget.transform;
        else
            target = null;

    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target.position, 5f * Time.deltaTime);
            skillFly.gameObject.SetActive(false);
        }
        else
        {
            skillFly.gameObject.SetActive(true);
        }
      
    }
}
