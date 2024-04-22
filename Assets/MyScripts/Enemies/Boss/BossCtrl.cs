using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : EnemiesCtrl
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }
}
