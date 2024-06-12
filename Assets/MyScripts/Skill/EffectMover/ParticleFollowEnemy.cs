using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowEnemy : RPGMonoBehaviour
{

    // Update is called once per frame
    protected Transform player;
    [SerializeField] protected Transform target;
    protected override void LoadComponents()
    {
        LoadPlayer();
    }
    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void OnEnable()
    {
        if (PlayerCtrl.theTarget != null)
        {
            target = PlayerCtrl.theTarget.transform;
            transform.position = target.position;
        }
        else
        {
            target = null;
            transform.position = player.position;
        }
       

    }

    
}
