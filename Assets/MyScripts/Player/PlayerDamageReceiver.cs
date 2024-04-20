using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : HitableObjectDamageReceiver
{

    protected override void OnDead()
    {
        Debug.Log("Bạn đã chết");
    }
}
