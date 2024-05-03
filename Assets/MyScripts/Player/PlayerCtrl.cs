using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : HitableObjectCtrl
{

    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}


