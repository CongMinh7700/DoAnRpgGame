using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : HitableObjectDamageReceiver
{
    private int baseHpMax;
    private double baseDefense;
    //Mỗi lần leveup update lại hpMax
    private void Start()
    {
        baseHpMax = hpMax;
        baseDefense = defense;
    }

    void UpdateBase()
    {
        baseHpMax = hpMax;
        baseDefense = defense;
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
    }
}
