﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : Level
{
    protected static LevelSystem instance;
    public static LevelSystem Instance => instance;
    public int currentXp;
    public int requireXp;
    public int number;
    [Header("Stat")]
    public float additionMultiple = 300;
    public float powerMultiple = 2;
    public float divisionMultiple = 7;
    public PlayerCtrl playerCtrl;
    public static int damageLevel;
    protected override void Awake()
    {
        if (LevelSystem.instance != null) Debug.Log("Only 1 Level System Allow to exits");
        LevelSystem.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerCtrl();
    }
    public virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        UpdatePlayerStatus(0);

    }
    private void Start()
    {
        UpdatePlayerStatus(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GainExperienceFlatRate(100);
        }
        if (currentXp < requireXp) return;
        LevelUp();
    }
    //Number ByLevel
    public void GainExperienceFlatRate(int xpGained)
    {
        currentXp += xpGained;
        Debug.Log("Call Xp");
    }
    public override bool SetLevel(int level)
    {
        bool status = base.SetLevel(level);
        return status;
    }

    public override bool LevelUp()
    {
        bool status = base.LevelUp();
        currentXp = Mathf.RoundToInt(currentXp - requireXp);
        requireXp = RequireXpByNumber();
        CreateNotificationLevel();
        UpdatePlayerStatus(levelCurrent);
        return status;
    }

    protected int RequireXpByNumber()
    {
        for(int i=0;i< levelCurrent; i++)
        {
            number += (int)Mathf.Floor(i + additionMultiple * Mathf.Pow(powerMultiple, i / divisionMultiple));
        }

        return number/4;
    }
    protected virtual void UpdatePlayerStatus(int level)
    {
        playerCtrl.DamageReceiver.SetHpMax(playerCtrl.PlayerSO.hpMax+level * 10);
        playerCtrl.DamageReceiver.SetDefense(playerCtrl.PlayerSO.defense + (level * 0.1));
        playerCtrl.PlayerAttack.SetStaminaMax(playerCtrl.PlayerSO.stamina + level * 10);
        playerCtrl.UsingSkill.SetManaMax(playerCtrl.PlayerSO.mana + level * 10);
        damageLevel = playerCtrl.PlayerSO.damage + level;
    }
    //Sound nữa thì đẹp
    protected virtual void CreateNotificationLevel()
    {
        string fxName = FXSpawner.notification;

        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        NotificationText nofText = fxObj.GetComponentInChildren<NotificationText>();
        nofText.SetText("Level Up");
        fxObj.gameObject.SetActive(true);
    }

}
