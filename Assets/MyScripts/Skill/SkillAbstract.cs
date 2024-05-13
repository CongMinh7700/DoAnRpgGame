using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAbstract : RPGMonoBehaviour
{
    [SerializeField] protected AttackSkillCtrl skillCtrl;

    protected override void LoadComponents()
    {
        this.LoadSkillCtrl();
    }
    protected virtual void LoadSkillCtrl()
    {
        if (this.skillCtrl != null) return;
        this.skillCtrl = GetComponentInParent<AttackSkillCtrl>();

    }
}
