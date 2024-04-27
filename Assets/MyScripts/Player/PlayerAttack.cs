using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : RPGMonoBehaviour
{
    [SerializeField] protected PlayerAnim playerAnim;

    protected override void LoadComponents()
    {
        this.LoadPlayerAnimation();
    }

    public virtual void LoadPlayerAnimation()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = transform.GetComponentInParent<PlayerAnim>();
        Debug.LogWarning(transform.name + "||LoadPlayerAnimation||", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.LoadTrail();
        this.Attacking();
    }
    public void Attacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.AttackAnimation("Spear");
        }
    }
}
