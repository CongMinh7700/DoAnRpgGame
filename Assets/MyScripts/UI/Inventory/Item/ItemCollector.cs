using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : RPGMonoBehaviour// chưa dung
{
    //public cai nay roi add item vao
    public Item item;
    private readonly Vector3 rotAxis = new Vector3(0.1f, 1, 0.1f);

    private void Update()
    {
        transform.Rotate(rotAxis, Time.deltaTime * 200);

    }
    public void Create(Item item)
    {
        this.item = item;
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponentInParent<Interactor>();
        if (interactor != null) interactor.AddToInventory(item, gameObject);
    }
}
