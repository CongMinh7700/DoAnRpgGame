using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingSkill : MonoBehaviour
{
   public virtual void FireBall()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        string prefabName = SkillSpawner.Instance.fireBall;

        Transform newFireBall = SkillSpawner.Instance.Spawn(prefabName, position, rotation);
        if (newFireBall == null) return;
        newFireBall.gameObject.SetActive(true);
        SkillCtrl skillCtrl = newFireBall.GetComponent<SkillCtrl>();
        //skillCtrl.SetShooter(transform);
    } 
}
