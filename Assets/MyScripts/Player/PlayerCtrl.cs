using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : HitableObjectCtrl
{
    [Header("PlayerSO")]
    public PlayerSO playerSO;
    public Interactor interactor;
    public Transform spawnPoint;
    public PlayerAttack playerAttack;
    public UsingSkill usingSkill;
    public PlayerAnim playerAnim;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadInteractor();
        this.LoadPlayerAttack();
        this.LoadUsingSkill();
        this.LoadPlayerAnim();
    }
    protected virtual void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        string resPath = "HitableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(resPath);
        //Debug.LogWarning(transform.name + "||LoadSO||" + resPath, gameObject);
    }
    public virtual void LoadInteractor()
    {
        if (this.interactor != null) return;
        this.interactor = GetComponent<Interactor>();
    }
    protected virtual void LoadPlayerAttack()
    {
        if (this.playerAttack != null) return;
        this.playerAttack = GetComponentInChildren<PlayerAttack>();
    }
    protected virtual void LoadUsingSkill()
    {
        if (this.usingSkill != null) return;
        this.usingSkill = GetComponentInChildren<UsingSkill>();

    }
    protected virtual void LoadPlayerAnim()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = GetComponent<PlayerAnim>();
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}


