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
        currencyText.text = MoneyManager.Instance.Gold.ToString();
    }
    public void BuyWeaponButton()
    {
        if(MoneyManager.Instance.Gold >= cost)
        {
            inventory.inventoryEvents.AddItem(item);
            MoneyManager.Instance.MinusGold(cost);
            currencyText.text = MoneyManager.Instance.Gold.ToString() +" $";
            Debug.Log(transform.name + " Cost = " + cost);
        }
     
    }
}
