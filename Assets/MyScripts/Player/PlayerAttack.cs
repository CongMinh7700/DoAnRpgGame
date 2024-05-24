using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int staminaMax;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected int staminaCost;
    [SerializeField] protected int weaponIndex;
    public static bool canAttack;
    public int StaminaMax => staminaMax;
    public float CurrentStamina => currentStamina;
    [Header("Player State Info")]
    private AnimatorStateInfo playerInfo;
    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        this.staminaMax = playerCtrl.PlayerSO.stamina;
        this.currentStamina = staminaMax;
    }
    void Update()
    {

        playerCtrl.PlayerAnim.LoadTrail();
        this.Attacking();
        StaminaRecover();
        playerInfo = playerCtrl.PlayerAnim.Animator.GetCurrentAnimatorStateInfo(0);
    }
    public void Attacking()
    {
        string name = ItemManager.weaponName;
        switch (name)
        {
            case "Spear":
                staminaCost = 20;
                weaponIndex = 0;
                break;
            case "LongSword":
                staminaCost = 15;
                weaponIndex = 1;
                break;
            case "LongAxe":
                staminaCost = 25;
                weaponIndex = 2;
                break;
            default:
                return;

        }

        if (Input.GetMouseButtonDown(0) && currentStamina >= staminaCost)
        {
            if (!playerCtrl.PlayerAnim.isAttacking)
            {
                playerCtrl.PlayerSFX.SetWeaponSFX(weaponIndex);
                playerCtrl.PlayerAnim.AttackAnimation(name);
                StartCoroutine(ApplyStaminaAfterAnimation(staminaCost));
            }

        }
    }
   

    public virtual void StaminaRecover()
    {
        this.currentStamina += 5 * Time.deltaTime;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
    public virtual void StaminaDeduct(int value)
    {
        if (currentStamina >= value)
        {
            this.currentStamina -= value;
            if (currentStamina < 0) currentStamina = 0;
        }

    }
    public virtual void SetStaminaMax(int maxStamina)
    {
        this.staminaMax = maxStamina;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
    public virtual void SetCurrentStamina(float staminaValue)
    {
        this.currentStamina = staminaValue;
        if (this.currentStamina >= staminaMax) currentStamina = staminaMax;
    }
    IEnumerator ApplyStaminaAfterAnimation(int cost)
    {
        yield return new WaitUntil(() => !playerCtrl.PlayerAnim.IsPlayingAttackAnimation());
        StaminaDeduct(cost);
        playerCtrl.PlayerAnim.isAttacking = false;
    }
}
