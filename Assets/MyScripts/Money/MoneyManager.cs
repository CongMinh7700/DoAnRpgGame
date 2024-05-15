using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : RPGMonoBehaviour
{
    protected static MoneyManager instance;
    public static MoneyManager Instance => instance;
    protected override void Awake()
    {
        if (MoneyManager.instance != null) Debug.LogWarning("Only 1 MoneyManager Allow to exist");
        MoneyManager.instance = this;
    }
    [SerializeField] protected int gold;
    public  int Gold => gold;
    
    public virtual void AddGold(int value)
    {
        this.gold += value;
    }
    public virtual void MinusGold(int value)
    {
        this.gold -= value;
        if (gold <= 0) gold = 0;
    }
}
