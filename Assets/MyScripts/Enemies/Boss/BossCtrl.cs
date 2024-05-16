using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : EnemyCtrl
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }
}
