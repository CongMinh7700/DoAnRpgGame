using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Xử lý nhặt đồ
public class Interactor : RPGMonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public ItemContainer inventory;
    public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

    public bool AddToInventory(Item item, GameObject instance)
    {
        if (inventory.AddItem(item))
        {
            if (instance) Destroy(instance);
            return true;
        }
        return false;
    }
   
}
