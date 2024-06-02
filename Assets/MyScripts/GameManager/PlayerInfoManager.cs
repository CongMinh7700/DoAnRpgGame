using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfoManager : RPGMonoBehaviour
{
    [SerializeField] protected static PlayerInfoManager instance;
    public static PlayerInfoManager Instance => instance;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [Header("Components")]
    [SerializeField] protected TextMeshProUGUI playerName;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected TextMeshProUGUI healText;
    [SerializeField] protected Image staminaBar;
    [SerializeField] protected TextMeshProUGUI staminaText;
    [SerializeField] protected Image manaBar;
    [SerializeField] protected TextMeshProUGUI manaText;
    [SerializeField] protected Image xpBar;
    [SerializeField] protected TextMeshProUGUI xpText;
    [Header("Attributes")]
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currenHp;
    [SerializeField] protected int maxStamina;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected int maxMana;
    [SerializeField] protected float currentMana;
    [SerializeField] protected int requireXP;
    [SerializeField] protected float currentXP;



    [Header("Static")]
    public static string playerNameData;
    private void Start()
    {
        //playerNameData = "AintCming";//Text
        playerName.text = playerNameData + " - Level : " + LevelSystem.Instance.LevelCurrent;//Bỏ vô Update
    }
    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }

    public virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    private void Update()
    {
        playerName.text = playerNameData + " - Level : " + LevelSystem.Instance.LevelCurrent;
        this.maxHp = playerCtrl.DamageReceiver.HPMax;
        this.currenHp = playerCtrl.DamageReceiver.CurrentHp;
        this.maxStamina = playerCtrl.PlayerAttack.StaminaMax;
        this.currentStamina = playerCtrl.PlayerAttack.CurrentStamina;
        this.maxMana = playerCtrl.UsingSkill.ManaMax;
        this.currentMana = playerCtrl.UsingSkill.CurrentMana;
        this.currentXP = LevelSystem.Instance.currentXp;
        this.requireXP = LevelSystem.Instance.requireXp;
        SetUI();


    }
    protected virtual void SetUI()
    {
        this.healthBar.fillAmount = (float)currenHp / maxHp;
        this.healText.text = currenHp.ToString() + "/" + maxHp.ToString();

        this.staminaBar.fillAmount = (float)currentStamina / maxStamina;
        this.staminaText.text = currentStamina.ToString() + "/" + maxStamina.ToString();

        this.manaBar.fillAmount = (float)currentMana / maxMana;
        this.manaText.text = currentMana.ToString() + "/" + maxMana.ToString();

        this.xpBar.fillAmount = (float)currentXP / requireXP;
        this.xpText.text = currentXP.ToString() + "/" + requireXP.ToString();
    }
}
