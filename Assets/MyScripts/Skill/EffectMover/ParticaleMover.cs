using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleMover : RPGMonoBehaviour
{
    // Update is called once per frame
    protected GameObject player;
    public bool Shield;
    protected override void OnEnable()
    {
        if(Shield == true)
        {
            PlayerCtrl.shieldOn = true;
        }
    }
    protected override void LoadComponents()
    {
        LoadPlayer();
    }
    protected  virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = player.transform.position;
    }
}
