using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFly : RPGMonoBehaviour
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected Vector3 direction = Vector3.forward;

    protected virtual void Update()
    {
        this.Fly();
    }
    protected virtual void Fly()
    {
        transform.parent.Translate(direction * this.speed * Time.deltaTime,Space.Self);
    }
}
