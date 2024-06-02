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
        if (target != null)
        {
            // Tạo một điểm cao hơn trên đầu của mục tiêu
            Vector3 targetHeadPosition = target.position + Vector3.up * 1f;
            transform.position = Vector3.LerpUnclamped(transform.position, targetHeadPosition, 2f * Time.deltaTime);
            skillFly.gameObject.SetActive(false);
        }
        else
        {
            skillFly.gameObject.SetActive(true);
        }
    }
}
