using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : HitableObjectCtrl
{
    [Header("PlayerSO")]
    public PlayerSO playerSO;
    public List<WeaponCtrl> damageSenders;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
    }
    protected virtual void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        string resPath = "HitableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(resPath);
        Debug.LogWarning(transform.name + "||LoadSO||" + resPath, gameObject);
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}


