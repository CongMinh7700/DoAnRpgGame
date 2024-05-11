using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HintSkillMessage : RPGMonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected GameObject Info;
    [SerializeField] protected bool displaying = true;
    [SerializeField] protected Vector3 screenPoint;
    [SerializeField] protected SkillSlot skillSlot;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillSlot();
    }
    protected virtual void LoadSkillSlot()
    {
        if (this.skillSlot != null) return;
        this.skillSlot = GetComponentInParent<SkillSlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (displaying)
        {
            if (skillSlot.slotItem == null) return;
            Info.SetActive(true);
            Info.GetComponentInChildren<TextMeshProUGUI>().text = skillSlot.slotItem.itemInformation;
            screenPoint.x = Input.mousePosition.x + 100f;


        }
        screenPoint.y = Input.mousePosition.y;
        screenPoint.z = 1f;
        Info.transform.position = screenPoint;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Info.SetActive(false);
    }

}
