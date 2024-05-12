using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFly : ParentFly
{
    protected override void ResetValue()
    {
        this.speed = 5f;
    }
}
