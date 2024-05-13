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
        public static bool isUsingFireBall;
        public static bool canUsingHeal;
        [SerializeField] protected QuickBar quickBar;

        public Image fillImage;
    
        private void Update()
        {
            if (slotItem == null) return;
            UseItem();
        }
        public virtual void UseItem()
        {
            if(slotItem.itemName == "FireBall")
            {
                if (Input.GetKeyDown(key) && Time.timeScale == 1 && useDelay <= 0)
                {

                    UsingFireBall();

                }
                this.FireBallCoolDown();
            }
            if(slotItem.itemName == "Heal")
            {
                if (Input.GetKeyDown(key) && Time.timeScale == 1 && useDelay <= 0)
                {
                    UsingHeal();
                
                }
                HealCoolDown();
            }
        }
        private void UsingFireBall()
        {
            if (slotItem == null) return;
            if (slotItem.type == ItemType.Skill)
            {
                isUsingFireBall = true;
                this.useDelay = 2f;
            }
      
        }
        private void UsingHeal()
        {
            if (slotItem == null) return;
            if (slotItem.type == ItemType.Skill && slotItem.itemName == "Heal")
            {
                canUsingHeal = true;
                this.useDelay = 2f;
            }
        }
        protected virtual void FireBallCoolDown()
        {
            useDelay -= Time.deltaTime;
            if (useDelay <= 0)
            {
                fillImage.fillAmount = 0f;
                useDelay = 0f;
                useTimes = 0.5f;
            }
            if (isUsingFireBall == true)
            {
                this.useTimes -= Time.deltaTime;
                if (useTimes <= 0)
                {
                    isUsingFireBall = false;

                }
            }
            this.fillImage.fillAmount = this.useDelay / 2f;
        }
        protected virtual void HealCoolDown()
        {
            useDelay -= Time.deltaTime;
            if (useDelay <= 0)
            {
                fillImage.fillAmount = 0f;
                useDelay = 0f;
                useTimes = 0.5f;
            }
            if (canUsingHeal == true)
            {
                this.useTimes -= Time.deltaTime;
                if (useTimes <= 0)
                {
                    canUsingHeal = false;

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
