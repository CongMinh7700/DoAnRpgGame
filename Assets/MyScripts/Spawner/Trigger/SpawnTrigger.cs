using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [Header("Spawn Trigger")]
    
    protected  bool canSpawn;

    public bool CanSpawn => canSpawn;
    public bool isBoss = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isBoss)
        {
            canSpawn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSpawn = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isBoss)//nếu nhận nhiệm vụ thì mới spawn Boss
        {
            canSpawn = true;
        }
    }
}
