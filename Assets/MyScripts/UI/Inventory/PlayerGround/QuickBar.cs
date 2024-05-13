using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickBar : RPGMonoBehaviour
{
    [Header("Charater Stats")]
    public QuickItemSlot[] itemSlots;
    public QuickSkillSlot[] skillSlots;
    // public KeyCode[] key;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public Interactor containerInteractor;
    protected Transform containerPanel; //character Stats
    protected bool isUIInitialized;
    public PlayerCtrl playerCtrl;
    //PlayerInfo
    protected override void Awake()
    {
        //loi awake
        isUIInitialized = false;
        InitializeContainer();
        foreach (QuickItemSlot slot in itemSlots)
        {
            slot.Initialize();
        }
        foreach (QuickSkillSlot slot in skillSlots)
        {
            slot.Initialize();
        }
    }
    protected void InitializeContainer()
    {
        InitializeMainUI(transform);
        isUIInitialized = true;
    }
    protected void InitializeMainUI(Transform containerPanel)
    {
        this.containerPanel = containerPanel;
        mainContainerUI = this.containerPanel.Find("MainUI");
        AddQuickItemSlots();
        AddQuickSkilSlots();
       
    }
    private void AddQuickItemSlots()
    {
        Transform quickItemSlot = mainContainerUI.Find("SlotHolder").Find("QuickItemSlot");
        itemSlots = new QuickItemSlot[quickItemSlot.childCount];

        for (int i = 0; i < itemSlots.Length; i++)
        {
            QuickItemSlot slot = quickItemSlot.GetChild(i).GetComponent<QuickItemSlot>();
            itemSlots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
        }
    }
    private void AddQuickSkilSlots()
    {
        Transform quickSkillSLot = mainContainerUI.Find("SlotHolder").Find("SkillSlot");
        skillSlots = new QuickSkillSlot[quickSkillSLot.childCount];

        for (int i = 0; i < skillSlots.Length; i++)
        {
            QuickSkillSlot slot = quickSkillSLot.GetChild(i).GetComponent<QuickSkillSlot>();
            skillSlots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
        }
    }
    private void Update()
    {
        
    }
}


