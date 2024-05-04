using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsUpdate : RPGMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI hpText;
    [SerializeField] protected TextMeshProUGUI attackText;
    [SerializeField] protected TextMeshProUGUI defendText;
    [SerializeField] protected TextMeshProUGUI manaText;
    [SerializeField] protected TextMeshProUGUI staminaText;

    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected WeaponCtrl weaponCtrl;
 //   protected string name = "";
    private void Start()
    {
        nameText.text = "Tên : " +"AintCming1";
    }
    private void Update()
    {
        hpText.text = "Máu : "+ playerCtrl.DamageReceiver.HPMax.ToString();
        attackText.text = "Công : "+ playerCtrl.playerSO.damage.ToString();
        defendText.text = "Thủ : " + playerCtrl.DamageReceiver.Defense.ToString();
        manaText.text = "Mana : "+ playerCtrl.playerSO.mana.ToString();
        staminaText.text = "Stamina : "+ playerCtrl.playerSO.stamina.ToString();

    }
}
