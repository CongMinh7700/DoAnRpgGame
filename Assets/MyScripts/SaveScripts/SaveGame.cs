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
    [SerializeField] protected QuestDataManager questData;

    protected override void Awake()
    {
        if (SaveGame.instance != null) Debug.LogWarning("Only 1 Save Game allow to exist");
        SaveGame.instance = this;
    }
    public virtual void Save()
    {
        equipData.SaveData("");
        inventoryData.SaveData("");
        quickData.SaveData("");
        skillData.SaveData("");
        saveScripts.SaveData("");
        questData.SaveData("");

    }
    public virtual void Load()
    {
        equipData.LoadData("");
        inventoryData.LoadData("");
        quickData.LoadData("");
        skillData.LoadData("");
        saveScripts.LoadData("");
        questData.LoadData("");
    }
    protected override void LoadComponents()
    {
        LoadEquipData();
        LoadInventoryData();
        LoadQuickData();
        LoadSkillData();
        LoadPLayerSave();
        LoadQuestData();
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


}
