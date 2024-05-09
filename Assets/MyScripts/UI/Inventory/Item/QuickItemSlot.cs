using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuickItemSlot : ItemSlot
{
    [SerializeField] public KeyCode key;
    public ItemContainer inventory;
    [SerializeField] protected float useTimes = 0.5f;
    [SerializeField] protected float useDelay = 2f;
    [SerializeField] protected bool isUse ;

    public Image fillImage;
    private void Update()
    {
        UseItem();
    }
    public  virtual void UseItem()
    {
        if (Input.GetKeyDown(key) && Time.timeScale == 1 && useDelay <= 0)
        {
           
            ConsumeItem();
        }
        this.CoolDown();
    }
    private void ConsumeItem()
    {
        if (slotItem == null) return;
        if (slotItem.isFood )
        {
            BonusAttribute heal = slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "health");
            BonusAttribute mana = slotItem.bonusAttributes.FirstOrDefault(bonus => bonus.attributeName == "mana");
            if (heal != null)
            {
                //playerCtrl.DamageReceiver.Health(heal.attributeValue);
                Debug.Log("Heal");
            }
            if (mana != null)
            {
                Debug.Log("Use Mana");
            }
            isUse = true;
            this.useDelay = 2f;
        }
        Debug.Log("You have consumed" + slotItem.itemName);
        Remove(1);
    }
    protected virtual void CoolDown()
    {
         useDelay-= Time.deltaTime;

        if (useDelay <= 0)
        {
            fillImage.fillAmount = 0f;
            useDelay = 0f;
            useTimes = 0.5f;
        }
        if (isUse == true)
        {
            this.useTimes -= Time.deltaTime;
            if (useTimes <= 0)
            {
                isUse = false;

            }
        }
        this.fillImage.fillAmount = this.useDelay / 2f;
    }
    public virtual void BackToInventory()
    {
        if (!IsEmpty && slotItem != null)
        {
            if (slotItem.isFood)
            {
                inventory.inventoryEvents.AddAll(slotItem,itemCount);
                Clear();
            }
           
        }
    }
}

