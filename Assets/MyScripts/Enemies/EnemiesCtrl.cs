using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCtrl : HitableObjectCtrl
{
    
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
}
