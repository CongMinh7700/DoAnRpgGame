using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MessageManager : MonoBehaviour
{
    public GameObject[] shops;
    public int numbShop;
    public GameObject firstTask;
    public GameObject questTask;
    public TextMeshProUGUI dialogeText;
    public TextMeshProUGUI ownerText;
    public GameObject buttonRefuse;
    public GameObject buttonAccept;
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
    }
    public void HideButton()
    {
        buttonAccept.SetActive(false);
        buttonRefuse.SetActive(false);
    }
    public void Accept()
    {
        //Add Quest
    }
    public void Refuse()
    {
        //hide MessageBox 
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
