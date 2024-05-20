using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScripts : MonoBehaviour
{
    public static string playerName;
    public static GameObject target;
    public static Transform currentPosition;
    [Header("Current")]
    public static float currentMana;
    public static float currentStamina;
    public static float currentHealth;
    public static float curentExp;
    [Header("Player Status SO ")]
    public static float manaMax;
    public static float staminaMax;
    public static float healthMax;
    public static int damage;
    public static int defense;
    public static int maxExp;
    [Header("In Game")]
    public static string weaponName;//Xử lý trang bị vũ khí còn đồ thì nên để lưu trong equipSlot hoặc lưu luôn cả cái này
    public static int killAmount;
    public static int questIndex;
    [Header("Load Game")]
    public static bool saving = false;
    public static bool continueData = false;
    private bool checkForLoad = false;
    //Save ivnentory,EquipSlot,Quest,Money,QuickBar (Quick Skill,Item)



    [Header("Save")]
    public  string playerNameS;
    public  GameObject targetS;
    public  Transform currentPositionS;
    [Header("Current")]
    public  float currentManaS;
    public  float currentStaminaS;
    public  float currentHealthS;
    public  float curentExpS;
    [Header("Player Status SO ")]
    public  float manaMaxS;
    public  float staminaMaxS;
    public  float healthMaxS;
    public  int damageS;
    public  int defenseS;
    public  int maxExpS;
    [Header("In Game")]
    public  string weaponNameS;//Xử lý trang bị vũ khí còn đồ thì nên để lưu trong equipSlot hoặc lưu luôn cả cái này
    public  int killAmountS;
    public  int questIndexS;

}
