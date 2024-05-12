using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 25f;
    [SerializeField] protected float distance  = 0f;
    [SerializeField] protected Transform player;
    protected override void LoadComponents()
    {
        this.LoadPlayer();

    }
    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override bool CanDespawn()
    {
        distance = Vector3.Distance(transform.parent.position, player.position);
        if (distance > disLimit) return true;
        return false;
    }
}
