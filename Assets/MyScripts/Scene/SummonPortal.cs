using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPortal : MonoBehaviour
{
    public GameObject villagePortal;
    private void Update()
    {
        //Debug.LogWarning("CanSummon Portal" + LevelSystem.Instance.CanSummonPortal());
        if (LevelSystem.Instance.CanSummonPortal())
        {
            villagePortal.gameObject.SetActive(true);
        }
    }
}
