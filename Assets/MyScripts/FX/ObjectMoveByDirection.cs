using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveByDirection : MonoBehaviour
{
    public float offset = 5f;
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected Transform notification;
    [SerializeField] protected float speed;
    private void FixedUpdate()
    {
        GetMovePosition();
    }
    private void OnEnable()
    {
        targetPosition = Vector3.zero;
    }
    protected virtual void GetMovePosition()
    {
        Vector3 pos = transform.position;
        pos.y += offset;
        targetPosition = pos;
        Vector3 newPos = Vector3.Lerp(notification.position, pos, this.speed);
        notification.position = newPos;
    }
  
}
