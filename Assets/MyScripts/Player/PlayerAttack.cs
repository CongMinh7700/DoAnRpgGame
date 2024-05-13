using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : RPGMonoBehaviour
{
    [SerializeField] protected PlayerAnim playerAnim;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int staminaMax;
    [SerializeField] protected int currentStamina;

    public int StaminaMax => staminaMax;
    public int CurrentStamina => currentStamina;
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
    }
    void Update()
    {
        playerAnim.LoadTrail();
        this.Attacking();
    }
    public void Attacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.AttackAnimation(ItemManager.weaponName);
        }
    }

    public virtual void StaminaRecover(int value)
    {
        this.currentStamina += value;
        if (this.currentStamina > staminaMax) currentStamina = staminaMax;
    }
    public virtual void StaminaDeduct(int value)
    {
        this.currentStamina -= value;
        if (currentStamina < 0) currentStamina = 0;
    }
    public virtual void SetStaminaMax(int staminaMax)
    {
        this.staminaMax = staminaMax;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
}
