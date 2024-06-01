using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : RPGMonoBehaviour
{
    [Header("Charater Stats")]
    [SerializeField] private SlotOptions[] customOptionsMenuConfig;
    public KeyCode UIToggleKey = KeyCode.C;
    public SkillSlot[] slots;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public Interactor containerInteractor;
    [HideInInspector] public bool isContainerUIOpen = false;
    protected Transform containerPanel;
    protected bool isUIInitialized;
    protected override void Awake()
    {
        //loi awake
        isUIInitialized = false;
        InitializeContainer();
        foreach (SkillSlot slot in slots)
        {
            slot.Initialize();
        }
    }
    protected void Update()
    {
        // Debug.Log("Character stats :"+ isContainerUIOpen);
        if (isUIInitialized == false) return;
        CheckForUIToggleInput();
    }
    //Sinh ra 4 option 
    protected void InitializeContainer()
    {
        InitializeMainUI(transform);
        isUIInitialized = true;
    }
    protected void InitializeMainUI(Transform containerPanel)
    {
        this.containerPanel = containerPanel;
        mainContainerUI = this.containerPanel.Find("MainUI");
        GetCloseButton();

        Transform slotHolder = mainContainerUI.Find("SlotHolder");

        slots = new SkillSlot[slotHolder.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            SkillSlot slot = slotHolder.GetChild(i).GetComponent<SkillSlot>();
            slots[i] = slot;
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
        }
        mainContainerUI.gameObject.SetActive(false);
    }
    protected void GetCloseButton()
    {
        Button containerCloseButton = mainContainerUI.transform.Find("CloseButton").GetComponent<Button>();
        containerCloseButton.onClick.AddListener(() => ToggleUI());
    }
    //Create SlotOption

    public void CheckForUIToggleInput()
    {
        if (Input.GetKeyDown(UIToggleKey))
        {
            ToggleUI();
            Debug.Log("C");
        }
    }

    public void ToggleUI()
    {
        //Close
        SFXManager.Instance.PlaySFXClick();
        if (mainContainerUI.gameObject.activeSelf && isContainerUIOpen)
        {
            isContainerUIOpen = false;
            StartCoroutine(Utils.TweenScaleOut(mainContainerUI.gameObject, 50, false));
            Time.timeScale = 1;
        }
        else if (!mainContainerUI.gameObject.activeSelf && !isContainerUIOpen)
        {
            isContainerUIOpen = true;
            StartCoroutine(Utils.TweenScaleIn(mainContainerUI.gameObject, 50, Vector3.one));
            Time.timeScale = 0;
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i <= slots.Length; i++) if (slots[i].Add(item)) return true;
        return false;
    }
}
