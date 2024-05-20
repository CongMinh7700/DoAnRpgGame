﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : RPGMonoBehaviour
{
    [SerializeField] protected bool pauseTime =false;
    protected virtual  void FixedUpdate()
    {
        this.Despawning();
    }
   
    public virtual void Despawning()
    {
        
        if (!CanDespawn()) return;
        StartCoroutine(WaitToDespawn());
        if (pauseTime)
        {
            this.DespawnObject();
            pauseTime = false;
            UsingSkill.manaLow = false;
        }

    }
    public virtual void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }
    //Override lại tại skill không cần thiết
    IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(2f);
        pauseTime = true;

    }
    protected abstract bool CanDespawn();
}
