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
    //[SerializeField] protected WeaponCtrl weaponCtrl;
    //   protected string name = "";
    private void Start()
    {
        nameText.text = "Tên : " + "AintCming1";
    }
    private void Update()
    {
        levelText.text = "Level : " + (LevelSystem.Instance.LevelCurrent + 1).ToString();
        hpText.text = "Máu : " + playerCtrl.DamageReceiver.HPMax.ToString() + "(" + ItemManager.hpMaxBonus + ")";
        attackText.text = "Công : " + (LevelSystem.damageLevel + ItemManager.bonusAttack).ToString() + "(" + ItemManager.bonusAttack + ")";
        defendText.text = "Thủ : " + playerCtrl.DamageReceiver.Defense.ToString() + "(" + ItemManager.bonusDefense + ")";

        //Tạm thời chưa dùng
        manaText.text = "Mana : " + playerCtrl.PlayerSO.mana.ToString();
        staminaText.text = "Stamina : " + playerCtrl.PlayerSO.stamina.ToString();
        currencyText.text = MoneyManager.Instance.Gold.ToString() + " $";

    }
}
