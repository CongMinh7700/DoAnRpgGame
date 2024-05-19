using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillSlot : ItemSlot
{
    [SerializeField] public KeyCode key;
    protected float currentCooldown = 0f; // Thời gian cooldown của kỹ năng trong slot này
    public Image fillImage;
    public static bool canUsingFireBall;
    public static bool canUsingHeal;
    public static bool canUsingStrength;
    public UsingSkill usingSkill;
    private void Update()
    {
        if (slotItem == null) return;
        UseItem();
    }

    public virtual void UseItem()
    {
        Debug.Log("Can Use Skill :" + UsingSkill.canUseSkill);
        if (slotItem.type == ItemType.Skill)
        {
            // Lấy thời gian cooldown của kỹ năng từ dữ liệu của nó
            float skillCooldown = GetSkillCooldown(slotItem.itemName);

            if (currentCooldown <= 0f && Input.GetKeyDown(key) && Time.timeScale == 1 )
            {
                // Sử dụng kỹ năng
                switch (slotItem.itemName)
                {
                    case "FireBall":
                        UsingFireBall(skillCooldown);
                        
                        break;
                    case "Heal":
                        UsingHeal(skillCooldown);
                        break;
                    case "Strength":
                        UsingStrength(skillCooldown);
                        break;
                }
            }
        }
    }

    private void UsingFireBall(float cooldown)
    {
        // Thực hiện hành động của FireBall
        canUsingFireBall = true;
        currentCooldown = cooldown;
    }

    private void UsingHeal(float cooldown)
    {
        // Thực hiện hành động của Heal
        canUsingHeal = true;
        currentCooldown = cooldown;
    }

    private void UsingStrength(float cooldown)
    {
        // Thực hiện hành động của Strength
        canUsingStrength = true;
        currentCooldown = cooldown;
    }

    private void FixedUpdate()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
     
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            fillImage.fillAmount = currentCooldown / GetSkillCooldown(slotItem.itemName);
        }
        else 
        {
            fillImage.fillAmount = 0f;
        }
       
    }

    public virtual void BackToInventory()
    {
        if (!IsEmpty && slotItem != null)
            Clear();
    }
   

    private float GetSkillCooldown(string skillName)
    {
        
        switch (skillName)
        {
            case "FireBall":
                return 2f;
            case "Heal":
                return 5f;
            case "Strength":
                return 3f;
            default:
                return 0f;
        }
    }


}
