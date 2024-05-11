using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuickSkillSlot : ItemSlot
{
    [SerializeField] public KeyCode key;
    [SerializeField] protected float useTimes = 0.5f;
    [SerializeField] protected float useDelay = 2f;
    [SerializeField] protected bool isUse;

    public Image fillImage;
    private void Update()
    {
        UseItem();
    }
    public virtual void UseItem()
    {
        if (Input.GetKeyDown(key) && Time.timeScale == 1 && useDelay <= 0)
        {

            UsingSkill();
        }
        this.CoolDown();
    }
    private void UsingSkill()
    {
        if (slotItem == null) return;
        if (slotItem.type == ItemType.Skill)
        {
            Debug.Log("Using Skill");
            isUse = true;
            this.useDelay = 2f;
        }
      
    }
    protected virtual void CoolDown()
    {
        useDelay -= Time.deltaTime;

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
            Clear();
    }
}
