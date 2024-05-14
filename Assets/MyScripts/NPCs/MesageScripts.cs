using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MesageScripts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI buttonText;
    public Color32 messageOn;
    public Color32 messageOff;
    public GameObject[] shops;
    public int numbShop;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
    }
    private void Start()
    {
        shops[numbShop].SetActive(false);
    }
    public void Message2()
    {
        shops[numbShop].SetActive(true);
    }

}
