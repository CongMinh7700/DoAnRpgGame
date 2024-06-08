using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : HitableObjectDamageReceiver
{
    private int baseHpMax;
    private double baseDefense;
    //Mỗi lần leveup update lại hpMax
    public static bool isDeath = false;
    private void Start()
    {
        baseHpMax = hpMax;
        baseDefense = defense;
    }
        
    void UpdateBase()
    {
        baseHpMax = LevelSystem.hpMaxLevel;
        baseDefense = LevelSystem.defenseLevel;
    }
    private void Update()
    {
        UpdateBase();
        UpdateHpMax();

    }
    public void UpdateHpMax()
    {
        SetHpMax(baseHpMax + ItemManager.hpMaxBonus);
        SetDefense(baseDefense + ItemManager.bonusDefense);
    }
    //Quyết định tăng hp ở đây
    protected override void OnDead()
    {
        Debug.Log("Bạn đã chết");
        //Lose Screen
        isDeath = true;
    }
}
