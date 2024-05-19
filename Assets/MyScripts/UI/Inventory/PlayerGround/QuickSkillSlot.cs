using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillSlot : ItemSlot
{
    [SerializeField] public KeyCode key;
    protected float currentCooldown = 0f; 
    public Image fillImage;
    public UsingSkill usingSkill;

    private void Update()
    {
        if (slotItem == null) return;
        UseItem();
    }

    public virtual void UseItem()
    {
        if (slotItem.type == ItemType.Skill)
        {
            float skillCooldown = GetSkillCooldown(slotItem.itemName);

            if (currentCooldown <= 0f && Input.GetKeyDown(key) && Time.timeScale == 1)
            {
                bool skillUsed = false;

                // Sử dụng kỹ năng
                switch (slotItem.itemName)
                {
                    case "FireBall":
                        skillUsed = usingSkill.FireBall();
                        break;
                    case "Heal":
                        skillUsed = usingSkill.Heal();
                        break;
                    case "Strength":
                        skillUsed = usingSkill.Strength();
                        break;
                    case "Shield":
                        skillUsed = usingSkill.Shield();
                        break;
                    case "IceShard":
                        skillUsed = usingSkill.IceShard();
                        break;
                }

                if (skillUsed)
                {
                    currentCooldown = skillCooldown;
                }
            }
        }
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

    //Duy trì hiệu ứng nên cho bên despawn
    private float GetSkillCooldown(string skillName)
    {

        switch (skillName)
        {
            case "FireBall":
                return 3f;
            case "Heal":
                return 10f;
            case "Strength":
                return 5f;
            case "Shield":
                return 30f;
            default:
                return 0f;
        }
    }


}
