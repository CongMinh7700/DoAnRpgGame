using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerArea : RPGMonoBehaviour
{
    public GameObject area;
    public BoxCollider boxCollider;

    private void Start()
    {
        area.SetActive(false);
    }
    protected override void LoadComponents()
    {
        LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            area.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            area.SetActive(false);
        }
    }
}
