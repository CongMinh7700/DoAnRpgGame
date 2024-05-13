﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : RPGMonoBehaviour
{
    [SerializeField] protected PlayerAnim playerAnim;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int staminaMax;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected int staminaCost;

    public int StaminaMax => staminaMax;
    public float CurrentStamina => currentStamina;

    [Header("Player State Info")]
    private AnimatorStateInfo playerInfo;
    protected override void LoadComponents()
    {
        this.LoadPlayerAnimation();
        this.LoadPlayerCtrl();
    }

    public virtual void LoadPlayerAnimation()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = transform.GetComponentInParent<PlayerAnim>();
        Debug.LogWarning(transform.name + "||LoadPlayerAnimation||", gameObject);
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        this.staminaMax = playerCtrl.playerSO.stamina;
        this.currentStamina = staminaMax;
    }
    void Update()
    {
        playerAnim.LoadTrail();
        this.Attacking();
        StaminaRecover();
        playerInfo = playerAnim.Animator.GetCurrentAnimatorStateInfo(0);
    }
    public void Attacking()
    {
        string name = ItemManager.weaponName;
        switch (name)
        {
            case "Spear":
                staminaCost = 25;
                break;
            case "LongSword":
                staminaCost = 20;
                break;
            case "LongAxe":
                staminaCost = 30;
                break;
            default:
                return;

        }
        if (Input.GetMouseButtonDown(0) && currentStamina >= staminaCost)
        {
            if (!playerAnim.isAttacking)
            {
                playerAnim.AttackAnimation(name);
                StartCoroutine(ApplyStaminaAfterAnimation(staminaCost));
            }
            
        }
    }

    public virtual void StaminaRecover()
    {
        this.currentStamina += 0.1f ;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
    public virtual void StaminaDeduct(int value)
    {
        if(currentStamina >= value)
        {
            this.currentStamina -= value;
            if (currentStamina < 0) currentStamina = 0;
        }
    
    }
    public virtual void SetStaminaMax(int staminaMax)
    {
        this.staminaMax = staminaMax;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
    IEnumerator ApplyStaminaAfterAnimation(int cost)
    {
        yield return new WaitUntil(() => !playerAnim.IsPlayingAttackAnimation());
        StaminaDeduct(cost);
        playerAnim.isAttacking = false;
    }
}
