using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleMoveToPlayer : RPGMonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected SkillFly skillFly;
    protected override void LoadComponents()
    {
        LoadSkillFly();
        LoadPlayer();
    }
    protected virtual void LoadSkillFly()
    {
        if (this.skillFly != null) return;
        this.skillFly = GetComponentInChildren<SkillFly>();
    }
    public virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        if (player != null)
        {
            // Tạo một điểm cao hơn trên đầu của mục tiêu
            Vector3 targetHeadPosition = player.position + Vector3.up * 1f;
            transform.position = Vector3.LerpUnclamped(transform.position, targetHeadPosition, 2f * Time.deltaTime);
            skillFly.gameObject.SetActive(false);
        }
        else
        {
            skillFly.gameObject.SetActive(true);
        }
    }
}
