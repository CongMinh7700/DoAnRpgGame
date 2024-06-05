using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : RPGMonoBehaviour
{
    [Header("Charater Stats")]
    [SerializeField] private SlotOptions[] customOptionsMenuConfig;
    public KeyCode UIToggleKey = KeyCode.Q;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public bool isContainerUIOpen = false;
    protected Transform containerPanel;
    protected bool isUIInitialized;

    protected override void Awake()
    {
        //loi awake
        isUIInitialized = false;
        InitializeContainer();

    }
    protected void Update()
    {
        // Debug.Log("Character stats :"+ isContainerUIOpen);
        if (isUIInitialized == false) return;
        CheckForUIToggleInput();
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
        GetCloseButton();
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
        }
    }
    public void ToggleUI()
    {
        //Close
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
}
