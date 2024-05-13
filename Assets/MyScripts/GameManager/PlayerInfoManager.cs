using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfoManager : RPGMonoBehaviour
{
    [SerializeField] protected static PlayerInfoManager instance;
    public static PlayerInfoManager Instance => instance;
    [SerializeField] protected TextMeshProUGUI playerName;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected TextMeshProUGUI healText;
    [SerializeField] protected Image staminaBar;
    [SerializeField] protected TextMeshProUGUI staminaText;
    [SerializeField] protected Image manaBar;
    [SerializeField] protected TextMeshProUGUI manaText;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currenHp;

    protected override void LoadComponents()
    {
        this.LoadPlayerCtrl();
    }

    public virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    //private void Start()
    //{

    //    //SetUI(currenHp,maxHp);
    //}
    private void Update()
    {
        this.maxHp = playerCtrl.DamageReceiver.HPMax;
        this.currenHp = playerCtrl.DamageReceiver.HP;
        SetUI();


    }
    protected virtual void SetUI()
    {
        this.healthBar.fillAmount = (float)currenHp /maxHp ;
        this.healText.text = currenHp.ToString() + "/" + maxHp.ToString();
        //this.staminaBar.fillAmount = playerCtrl.playerSO.stamina;

    }
}
