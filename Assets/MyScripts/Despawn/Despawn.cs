using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : RPGMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    public virtual void Despawning()
    {
        if (!CanDespawn()) return;
        this.DespawnObject();


    }
    public virtual void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }
    //Override lại tại skill không cần thiết

    protected abstract bool CanDespawn();
}
