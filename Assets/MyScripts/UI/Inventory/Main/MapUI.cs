using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : RPGMonoBehaviour
{

    [Header("MapInventory")]
    public KeyCode UIToggleKey = KeyCode.M;
    public Transform mainContainerUI; //MainUI
    [HideInInspector] public bool isContainerUIOpen = false;
    protected Transform containerPanel;
    protected bool isUIInitialized;
    public GameObject cameraMap;
    protected override void Awake()
    {
        //loi awake
        isUIInitialized = false;
        InitializeContainer();
        cameraMap.SetActive(false);
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
        SFXManager.Instance.PlaySFXClick();
        if (mainContainerUI.gameObject.activeSelf && isContainerUIOpen)
        {
            isContainerUIOpen = false;
            StartCoroutine(Utils.TweenScaleOut(mainContainerUI.gameObject, 50, false));
            Time.timeScale = 1;
            cameraMap.SetActive(false);
        }
        else if (!mainContainerUI.gameObject.activeSelf && !isContainerUIOpen)
        {
            mainContainerUI.transform.localPosition = Vector3.zero;
            isContainerUIOpen = true;
            StartCoroutine(Utils.TweenScaleIn(mainContainerUI.gameObject, 50, Vector3.one));
            Time.timeScale = 0;
            cameraMap.SetActive(true);
        }
    }
}

