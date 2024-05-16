﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MessageManager : MonoBehaviour
{
    public GameObject[] shops;
    public int numbShop;
    [Header("UI")]
    public GameObject firstTask;
    public GameObject questTask;
    public TextMeshProUGUI dialogeText;
    public TextMeshProUGUI ownerText;
    public GameObject buttonRefuse;
    public GameObject buttonAccept;

    public Quest currentQuest;
    private void Start()
    {
        shops[numbShop].SetActive(false);
    }
    public void Message2()
    {
        shops[numbShop].SetActive(true);
        firstTask.SetActive(false);
    }
    public void Message1()
    {
        questTask.SetActive(true);
        firstTask.SetActive(false);
    }
    public void ShowButton()
    {
        buttonAccept.SetActive(true);
        buttonRefuse.SetActive(true);
        Debug.Log("Show");
    }
    public void HideButton()
    {
        buttonAccept.SetActive(false);
        buttonRefuse.SetActive(false);
        Debug.Log("Hide");
    }
    public void Accept()
    {
        //Add Quest
        if (currentQuest == null) return;
        if (currentQuest.questState == QuestState.NotStarted && currentQuest!= null)
        {
            QuestManager.Instance.AddQuest(currentQuest);
            QuestManager.Instance.UpdateQuestLog();
            currentQuest = null;
        }

       
    }
    public void Refuse()
    {
        //hide MessageBox 
        questTask.SetActive(false);
        firstTask.SetActive(true);
    }
    private void Update()
    {
        if(numbShop == 0)
        {
            ownerText.text = " Thợ Rèn";
        }
        if( numbShop == 1)
        {
            ownerText.text = "Ông chủ";
        }
        if (numbShop == 2)
        {
            ownerText.text = "Phù Thủy";
        }
        else return;

       
    }

}
