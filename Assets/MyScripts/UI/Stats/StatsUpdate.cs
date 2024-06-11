using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsUpdate : RPGMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI levelText;
    [SerializeField] protected TextMeshProUGUI hpText;
    [SerializeField] protected TextMeshProUGUI attackText;
    [SerializeField] protected TextMeshProUGUI defendText;
    [SerializeField] protected TextMeshProUGUI manaText;
    [SerializeField] protected TextMeshProUGUI staminaText;
    [SerializeField] protected TextMeshProUGUI currencyText;

    [SerializeField] protected PlayerCtrl playerCtrl;
    private void Start()
    {
        nameText.text = "Tên : " + PlayerInfoManager.playerNameData;
    }
    private void Update()
    {
        levelText.text = "Level : " + (LevelSystem.Instance.LevelCurrent + 1).ToString();
        hpText.text = "Máu : " + playerCtrl.DamageReceiver.HPMax.ToString() + "(" + ItemManager.hpMaxBonus + ")";
        attackText.text = "Công : " + (LevelSystem.damageLevel + ItemManager.bonusAttack).ToString() + "(" + ItemManager.bonusAttack + ")";
        defendText.text = "Thủ : " + playerCtrl.DamageReceiver.Defense.ToString() + "(" + ItemManager.bonusDefense + ")";
        manaText.text = "Mana : " + playerCtrl.UsingSkill.ManaMax.ToString()+"("+ItemManager.manaBonus+")";
        staminaText.text = "Stamina : " + playerCtrl.PlayerAttack.StaminaMax.ToString()+"("+ItemManager.staminaBonus+")";
        currencyText.text = MoneyManager.Instance.Gold.ToString() + " $";

    }
}
