using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : HitableObjectCtrl
{
    [Header("PlayerSO")]
    public PlayerSO playerSO;
    public Interactor interactor;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadInteractor();
    }
    protected virtual void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        string resPath = "HitableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(resPath);
        Debug.LogWarning(transform.name + "||LoadSO||" + resPath, gameObject);
    }
    public virtual void LoadInteractor()
    {
        if (this.interactor != null) return;
        this.interactor = GetComponent<Interactor>();

    }

    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}


