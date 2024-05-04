using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitableObjectCtrl : RPGMonoBehaviour
{
    //Thieu Despawn ,Spawner
   
    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;
    [SerializeField] protected HitableObjectSO hitableObjectSO;
    public HitableObjectSO HitableObjectSO => hitableObjectSO;

   

    protected override void LoadComponents()
    {
        this.LoadDamageReceiver();
        this.LoadSO();
    }

    public virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = GetComponentInChildren<DamageReceiver>();
        Debug.LogWarning(transform.name + "|LoadDamageReceiver|", gameObject);

    }

    public virtual void LoadSO()
    {
        if (this.hitableObjectSO != null) return;
        string resPath = "HitableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.hitableObjectSO = Resources.Load<HitableObjectSO>(resPath) ;
        Debug.LogWarning(transform.name + "||LoadSO||" + resPath, gameObject);
    }
    
    protected abstract string GetObjectTypeString();
}
