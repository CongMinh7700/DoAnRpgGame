using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickBar : RPGMonoBehaviour
{
    [Header("Charater Stats")]
    public QuickItemSlot[] slots;
    // public KeyCode[] key;
    public Item slotItem;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public Interactor containerInteractor;
    protected Transform containerPanel; //character Stats
    protected bool isUIInitialized;
    protected override void Awake()
    {
        //loi awake
        isUIInitialized = false;
        InitializeContainer();
        foreach (QuickItemSlot slot in slots)
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
        Transform quickItemSlot = mainContainerUI.Find("SlotHolder").Find("QuickItemSlot");
        slots = new QuickItemSlot[quickItemSlot.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            QuickItemSlot slot = quickItemSlot.GetChild(i).GetComponent<QuickItemSlot>();
            slots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
        }
    }
   
    public bool AddItem(Item item)
    {
        for (int i = 0; i <= slots.Length; i++) if (slots[i].Add(item)) return true;
        return false;
    }
}
