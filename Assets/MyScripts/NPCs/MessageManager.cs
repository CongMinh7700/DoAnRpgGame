using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject[] shops;
    public int numbShop;
    public GameObject firstTask;
    public GameObject questTask;

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
}
