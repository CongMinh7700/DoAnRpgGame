using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScripts : MonoBehaviour
{
    //public static string playerName;
    //public static GameObject target;
    //public static Transform currentPosition;
    //[Header("Current")]
    //public static float currentMana;
    //public static float currentStamina;
    //public static float currentHealth;
    //public static float curentExp;
    //[Header("Player Status SO ")]
    //public static float manaMax;
    //public static float staminaMax;
    //public static float healthMax;
    //public static int damage;
    //public static int defense;
    //public static int maxExp;
    //[Header("In Game")]
    //public static string weaponName;//Xử lý trang bị vũ khí còn đồ thì nên để lưu trong equipSlot hoặc lưu luôn cả cái này
    //public static int killAmount;
    //public static int questIndex;
    //[Header("Load Game")]
    public static bool saving = false;
    public static bool continueData = false;
    private bool checkForLoad = false;
    //Quest,Money
    
    [Header("Save")]
    public  string playerNameS;
    public  GameObject targetS;
    public  Transform currentPositionS;
    public  PlayerCtrl playerCtrl;
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
    public  int killAmountS;
    public static int instance = 0;

    public virtual void SaveData(string id)
    {
        string dataPath = GetIDPath(id);
        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Existing data with id : " + id + "is overwriting");
        }
        try
        {
            PlayerData playerData = new PlayerData(); 
            playerData.playerName = PlayerInfoManager.playerNameData;
            playerData.currentMana = playerCtrl.UsingSkill.CurrentMana;
            playerData.currentHealth = playerCtrl.DamageReceiver.CurrentHp;
            playerData.currentStamina = playerCtrl.PlayerAttack.CurrentStamina;
            playerData.manaMax = playerCtrl.UsingSkill.ManaMax;
            playerData.healthMax = playerCtrl.DamageReceiver.HPMax;
            playerData.staminaMax = playerCtrl.PlayerAttack.StaminaMax;
            playerData.damage = playerCtrl.PlayerSO.damage;
            playerData.defense = playerCtrl.DamageReceiver.Defense;
            //playerData.currentExp = 0;
            //playerData.maxExp = 0;
            string jsonData = JsonUtility.ToJson(playerData);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data successfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list.");
        }


    }
    public virtual void LoadData(string id)
    {
        string dataPath = GetIDPath(id);
        if(!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data exists for the provided id: " + id);
            return;
        }
        try
        {
            string json = System.IO.File.ReadAllText(dataPath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            PlayerInfoManager.playerNameData = playerData.playerName;
            playerCtrl.UsingSkill.SetCurrentMana(playerData.currentMana);
            playerCtrl.DamageReceiver.SetCurentHp(playerData.currentHealth);
            playerCtrl.PlayerAttack.SetCurrentStamina(playerData.currentStamina);
            
            playerCtrl.UsingSkill.SetManaMax(playerData.manaMax);
            playerCtrl.DamageReceiver.SetHpMax(playerData.healthMax);
            playerCtrl.PlayerAttack.SetStaminaMax(playerData.staminaMax);
            playerCtrl.PlayerSO.damage = playerData.damage;
            playerCtrl.DamageReceiver.SetDefense(playerData.defense);
            //playerCtrl.PlayerExperience.MaxExp = playerData.maxExp; // Giả sử bạn có thuộc tính này
            //playerCtrl.PlayerExperience.CurrentExp = playerData.currentExp; // Giả sử bạn có thuộc tính này

            Debug.Log("Dữ liệu người chơi đã được tải.");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list.");
        }
    }
    public virtual void DeleteData()
    {

    }
    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/playerData_{id}.json";
    }
}
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float currentMana;
    public float currentStamina;
    public int currentHealth;
    public float currentExp;
    public int manaMax;
    public int staminaMax;
    public int healthMax;
    public int damage;
    public int defense;
    public int maxExp;
    public int killAmount;
}