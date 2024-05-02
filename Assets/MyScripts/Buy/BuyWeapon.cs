using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyWeapon : RPGMonoBehaviour
{
    [SerializeField] protected int cost;
    [SerializeField] protected TextMeshProUGUI currencyText;
    [SerializeField] protected Item item;
    [SerializeField] protected ItemContainer inventory;

    private void Start()
    {
        currencyText.text = "600";
    }
    public void BuyWeaponButton()
    {
        inventory.inventoryEvents.AddItem(item);
        currencyText.text = "300";
    }
}
