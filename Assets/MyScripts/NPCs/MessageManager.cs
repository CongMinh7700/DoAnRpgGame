using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MessageManager : RPGMonoBehaviour
{
    public GameObject[] shops;
    public int numbShop;
    [Header("UI")]
    public GameObject shopTalk;
    public GameObject firstTalk;
    public GameObject questTalk;
    public TextMeshProUGUI dialogeText;
    public TextMeshProUGUI ownerText;
    public GameObject buttonRefuse;
    public GameObject buttonAccept;
    public GameObject fText;

    public Quest currentQuest;
    public static bool isAccept;
    private void Start()
    {
        if (numbShop > 4) return;
        shops[numbShop].SetActive(false);
    }
    public void Message2()
    {
        SFXManager.Instance.PlaySFXClick();
        firstTalk.SetActive(false);
        if (numbShop > 4) return;
        shops[numbShop].SetActive(true);
        shopTalk.SetActive(false);
        HideButton();
    }
    public void Message1()
    {
        SFXManager.Instance.PlaySFXClick();
        if (numbShop > 4)
        {
            firstTalk.SetActive(false);
        }
        else
        {
            shopTalk.SetActive(false);
        }
        HideButton();
        fText.SetActive(true);
        questTalk.SetActive(true);
    }
    public void ShowButton()
    {
        buttonAccept.SetActive(true);
        buttonRefuse.SetActive(true);
        fText.SetActive(false);
        //Debug.Log("Show");
    }
    public void HideButton()
    {
        buttonAccept.SetActive(false);
        buttonRefuse.SetActive(false);
        //Debug.Log("Hide");
    }
    public void Accept()
    {
        //Add Quest
        if (currentQuest == null) return;
        SFXManager.Instance.PlaySFXClick();
        isAccept = true;
        if (currentQuest.questState == QuestState.NotStarted && currentQuest != null)
        {
            QuestManager.Instance.AddQuest(currentQuest);
            QuestManager.Instance.UpdateQuestLog();
            currentQuest = null;
        }

        questTalk.SetActive(false);
        if (numbShop > 4)
        {
            firstTalk.SetActive(true);
        }
        else
        {
            shopTalk.SetActive(true);
        }

    }
    public void Refuse()
    {
        SFXManager.Instance.PlaySFXClick();
        if (numbShop > 4)
        {
            firstTalk.SetActive(true);
        }
        else
        {
            shopTalk.SetActive(true);
        }
        questTalk.SetActive(false);

    }
    private void Update()
    {
        if (numbShop == 0 || numbShop == 3)
        {
            ownerText.text = " Thợ Rèn";
        }
        if (numbShop == 1 || numbShop == 4)
        {
            ownerText.text = "Ông chủ";
        }
        if (numbShop == 2)
        {
            ownerText.text = "Phù Thủy";
        }
        if (numbShop == 5 || numbShop == 6)
        {
            ownerText.text = "Trưởng làng";
        }
        else return;

    }
}
