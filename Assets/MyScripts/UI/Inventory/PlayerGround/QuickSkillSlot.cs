using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillSlot : ItemSlot
{
    [SerializeField] public KeyCode key;
    public float currentCooldown = 0f;
    public Image fillImage;
    public UsingSkill usingSkill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadUsingSkill();
    }
    protected   virtual void LoadUsingSkill()
    {
        if (this.usingSkill != null) return;
        this.usingSkill = FindObjectOfType<UsingSkill>();
    }
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
                    case "DimenBoom":
                        skillUsed = usingSkill.DimenBoom();
                        break;
                }
                if (skillUsed)
                {
                    if (slotItem.itemName == "Strength" || slotItem.itemName == "Shield")
                    {
                        StartCoroutine(WaitForManaLow(skillCooldown));
                    }
                    else
                    {
                        currentCooldown = skillCooldown;
                    }
             
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
        if (slotItem == null) return;
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
    private IEnumerator WaitForManaLow(float skillCooldown)
    {
        // Wait until manaLow becomes true
        yield return new WaitUntil(() => UsingSkill.manaLow);

        // Start the cooldown
        currentCooldown = skillCooldown;
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
                return 5f;
            case "IceShard":
                return 5f;
            case "Heal":
                return 45f;
            case "DimenBoom":
                return 7f;
            case "Strength":
                return 60f;
            case "Shield":
                return 75f;
            default:
                return 0f;
        }
    }


}
