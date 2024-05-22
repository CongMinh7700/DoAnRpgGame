using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : RPGMonoBehaviour
{
    // Update is called once per frame
    protected GameObject player;
    public bool Shield;
    public bool Strength;

    protected override void LoadComponents()
    {
        LoadPlayer();
    }
    protected  virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void OnEnable()
    {
        transform.position = player.transform.position;
        if (Shield == true)
        {
            PlayerCtrl.shieldOn = true;
        }
        if (Strength == true)
        {
            PlayerCtrl.strengthOn = true;
        }
    }

}
