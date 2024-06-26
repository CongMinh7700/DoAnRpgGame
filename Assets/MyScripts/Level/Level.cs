using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : RPGMonoBehaviour
{
    [Header("Level")]
    [SerializeField] protected int levelCurrent = 0;
    [SerializeField] protected int levelMax = 100;
    public int LevelCurrent => levelCurrent;
    public int LevelMax => levelMax;
   
    public virtual bool LevelUp()
    {
        int newLevel = this.levelCurrent + 1;
        if (newLevel > this.levelMax) return false;

        this.levelCurrent += 1;
        return true;
    }

    public virtual bool SetLevel(int newLevel)
    {
        if (newLevel > this.levelMax) return false;
        if (newLevel < 1) return false;
        this.levelCurrent = newLevel;
        return true;
    }
}
