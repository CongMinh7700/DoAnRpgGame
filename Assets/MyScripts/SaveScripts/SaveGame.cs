using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : RPGMonoBehaviour
{
    protected static SaveGame instance;
    public static SaveGame Instance => instance;
    [SerializeField] protected EquipSlotDataManager equipData;
    [SerializeField] protected InventoryDataManager inventoryData;
    [SerializeField] protected QuickSlotDataManager quickData;
    [SerializeField] protected SkillDataManager skillData;
    [SerializeField] protected SaveScripts saveScripts;
    public SaveScripts SaveScripts => saveScripts;
    [SerializeField] protected QuestDataManager questData;
    [SerializeField] protected LoadSceneManager loadSceneManager;
    public static bool newGame = false;
    protected override void Awake()
    {
        if (SaveGame.instance != null) Debug.LogWarning("Only 1 Save Game allow to exist");
        SaveGame.instance = this;
    }
    private void Start()
    {
        Debug.Log("New Game :" + newGame);
        if (!newGame )
        {
            if (HasData())
            {
                Debug.Log("Load");
                Load();
                //saveScripts.LoadData("");
            }
            else
            {
               // saveScripts.SaveData("");
                //saveScripts.LoadData("");
                Debug.Log("SaveScripts");
                return;
            }
        }
        else
        {
            DeleteData();
            saveScripts.SaveData("");
            saveScripts.LoadData("");
            StartCoroutine(WaitToFalse());
            LevelSystem.Instance.bossKill = 0;

        }
        Time.timeScale = 1;
    }
    public virtual void Save()
    {
        equipData.SaveData("");
        quickData.SaveData("");
        skillData.SaveData("");
        saveScripts.SaveData("");
        questData.SaveData("");
        loadSceneManager.SaveData("");
        inventoryData.SaveData("");
    }
    public virtual void Load()
    {
        Debug.LogWarning("Call Load Save Gaem");
        saveScripts.LoadData("");
        questData.LoadData("");
        equipData.LoadData("");
        quickData.LoadData("");
        skillData.LoadData("");
        inventoryData.LoadData("");

    }
    public virtual void DeleteData()
    {
        equipData.DeleteData("");
        quickData.DeleteData("");
        skillData.DeleteData("");
        saveScripts.DeleteData("");
        questData.DeleteData("");
        inventoryData.DeleteData("");
        Debug.Log("Delete Data");
    }
    public static bool HasData()
    {
        return EquipSlotDataManager.HasData("") ||
                QuickSlotDataManager.HasData("") ||
                SkillDataManager.HasData("") ||
                SaveScripts.HasData("") ||
                QuestDataManager.HasData("") ||
                InventoryDataManager.HasData("");
    }
    protected override void LoadComponents()
    {
        LoadEquipData();
        LoadInventoryData();
        LoadQuickData();
        LoadSkillData();
        LoadPLayerSave();
        LoadQuestData();
        LoadSceneManager();
    }
    protected virtual void LoadEquipData()
    {
        if (this.equipData != null) return;
        this.equipData = GetComponentInChildren<EquipSlotDataManager>();
    }
    protected virtual void LoadInventoryData()
    {
        if (this.inventoryData != null) return;
        this.inventoryData = GetComponentInChildren<InventoryDataManager>();
    }
    protected virtual void LoadQuickData()
    {
        if (this.quickData != null) return;
        this.quickData = GetComponentInChildren<QuickSlotDataManager>();
    }
    protected virtual void LoadSkillData()
    {
        if (this.skillData != null) return;
        this.skillData = GetComponentInChildren<SkillDataManager>();
    }
    protected virtual void LoadPLayerSave()
    {
        if (this.saveScripts != null) return;
        this.saveScripts = GetComponentInChildren<SaveScripts>();
    }
    protected virtual void LoadQuestData()
    {
        if (this.questData != null) return;
        this.questData = GetComponentInChildren<QuestDataManager>();
    }
    protected virtual void LoadSceneManager()
    {
        if (this.loadSceneManager != null) return;
        this.loadSceneManager = GetComponentInChildren<LoadSceneManager>();
    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(5f);
        newGame = false;
    }
}
