using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : RPGMonoBehaviour
{
    [SerializeField] protected EquipSlotDataManager equipData;
    [SerializeField] protected InventoryDataManager inventoryData;
    [SerializeField] protected QuickSlotDataManager quickData;
    [SerializeField] protected SkillDataManager skillData;
    [SerializeField] protected SaveScripts saveScripts;

    public virtual void Save()
    {
        equipData.SaveData("");
        inventoryData.SaveData("");
        quickData.SaveData("");
        skillData.SaveData("");
        saveScripts.SaveData("");

    }
    public virtual void Load()
    {
        equipData.LoadData("");
        inventoryData.LoadData("");
        quickData.LoadData("");
        skillData.LoadData("");
        saveScripts.LoadData("");
    }
    protected override void LoadComponents()
    {
        LoadEquipData();
        LoadInventoryData();
        LoadQuickData();
        LoadSkillData();
        LoadPLayerSave();
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


}
