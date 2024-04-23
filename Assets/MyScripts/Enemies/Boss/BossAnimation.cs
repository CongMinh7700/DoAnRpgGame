using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : EnemyAnimation
{
    public virtual void AttackCombo()
    {
        animator.SetTrigger("AttackCombo");
     
    }

}
