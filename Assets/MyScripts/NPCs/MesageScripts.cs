using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MesageScripts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Đổi màu text khi hover

    public TextMeshProUGUI buttonText;
    public Color32 messageOn;
    public Color32 messageOff;
   

    //SetText = dialogues của npc theo numbShop
    //Cho dialogueIndex ++;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
    }
  

}
