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

    protected override void LoadComponents()
    {
        this.LoadInventory();
    }
    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = FindObjectOfType<ItemContainer>();
    }
    //private void Update()
    //{
    //    //currencyText.text = MoneyManager.Instance.Gold.ToString();
    //}
    private void Start()
    {
        currencyText.text = MoneyManager.Instance.Gold.ToString() + " $";
    }
    public void BuyWeaponButton()
    {
        SFXManager.Instance.PlaySFXClick();
        if (MoneyManager.Instance.Gold >= cost)
        {
            SFXManager.Instance.PlaySFXPickUp();
            if(inventory.inventoryEvents.AddItem(item))
            {
                MoneyManager.Instance.MinusGold(cost);
            }
            currencyText.text = MoneyManager.Instance.Gold.ToString() +" $";
           // Debug.Log(transform.name + " Cost = " + cost);
            QuestManager.Instance.UpdateQuestProgress(item.itemName);
        }
     
    }
}
