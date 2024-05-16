using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : HitableObjectCtrl
{
    public string GetEnemyName()
    {
        return transform.name;
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
}
