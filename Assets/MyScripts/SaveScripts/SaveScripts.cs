using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveScripts : RPGMonoBehaviour
{

    public static bool saving = false;
    public static bool continueData = false;
    //private bool checkForLoad = false;
    [Header("Save")]
    public PlayerCtrl playerCtrl;
    public LevelSystem levelSystem;
    //[Header("In Game")]
    // public static int instance = 0;
    public virtual void SaveData(string id)
    {
        string dataPath = GetIDPath(id);
        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Existing SaveScripts data with id : " + id + "is overwriting");
        }
        try
        {
            PlayerData playerData = new PlayerData();
            playerData.level = levelSystem.LevelCurrent;
            playerData.playerName = PlayerInfoManager.playerNameData;
            playerData.manaMax = playerCtrl.UsingSkill.ManaMax;
            playerData.healthMax = playerCtrl.DamageReceiver.HPMax;
            playerData.staminaMax = playerCtrl.PlayerAttack.StaminaMax;
            playerData.currentMana = playerCtrl.UsingSkill.CurrentMana;
            playerData.currentHealth = playerCtrl.DamageReceiver.CurrentHp;
            playerData.currentStamina = playerCtrl.PlayerAttack.CurrentStamina;
            playerData.damage = LevelSystem.damageLevel;
            playerData.defense = playerCtrl.DamageReceiver.Defense;
            playerData.position = playerCtrl.transform.position;
            playerData.gold = MoneyManager.Instance.Gold;
            playerData.currentXp = levelSystem.currentXp;
            playerData.requireXp = levelSystem.requireXp;
            playerData.bossKill = levelSystem.bossKill;
            string jsonData = JsonUtility.ToJson(playerData);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data successfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the SaveScripts item list.");
        }


    }
    public virtual void LoadData(string id)
    {
        string dataPath = GetIDPath(id);
        if (!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data exists for the provided id: " + id);
            return;
        }
        try
        {
            string json = System.IO.File.ReadAllText(dataPath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            levelSystem.SetLevel(playerData.level);
            PlayerInfoManager.playerNameData = playerData.playerName;
            playerCtrl.UsingSkill.SetManaMax(playerData.manaMax);
            playerCtrl.DamageReceiver.SetHpMax(playerData.healthMax);
            playerCtrl.PlayerAttack.SetStaminaMax(playerData.staminaMax);
            LevelSystem.damageLevel = playerData.damage;
            playerCtrl.DamageReceiver.SetDefense(playerData.defense);
            playerCtrl.transform.position = playerData.position;
            MoneyManager.Instance.SetGold(playerData.gold);
            levelSystem.currentXp = playerData.currentXp;
            levelSystem.requireXp = playerData.requireXp;
            playerCtrl.UsingSkill.SetCurrentMana(playerData.currentMana);
            playerCtrl.DamageReceiver.SetCurentHp(playerData.currentHealth);
            playerCtrl.PlayerAttack.SetCurrentStamina(playerData.currentStamina);
            levelSystem.UpdatePlayerStatus(levelSystem.LevelCurrent);
            levelSystem.bossKill = playerData.bossKill;
            Debug.Log("<color=green>Data succesfully loaded! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the SaveScripts item list.");
        }
    }
    public void DeleteData(string id)
    {
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data with id: " + id + " is deleted.");
        }
    }

    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/playerData_{id}.json";
    }
    public static bool HasData(string id)
    {
        string dataPath =  Application.persistentDataPath + $"/playerData_{id}.json";
        return System.IO.File.Exists(dataPath);
    }
}
[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public string playerName;
    public float currentExp;
    public int manaMax;
    public int staminaMax;
    public int healthMax;
    public float currentMana;
    public float currentStamina;
    public int currentHealth;
    public int damage;
    public double defense;
    public int requireXp;
    public int currentXp;
    public int level;
    public int gold;
    public int bossKill;
}