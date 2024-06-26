using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShow : MonoBehaviour // Kh�ng d�ng
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(WaitoHide());
    }
    IEnumerator WaitoHide()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
