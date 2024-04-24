using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public  bool canSpawn;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
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
}
