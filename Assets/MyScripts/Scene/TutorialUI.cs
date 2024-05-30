using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : RPGMonoBehaviour
{
    public GameObject tutorialUI;
    protected override void LoadComponents()
    {
        LoadTutorialUI();
    }
    protected virtual void LoadTutorialUI()
    {
        if (this.tutorialUI != null) return;
        this.tutorialUI = gameObject;
    }
    public virtual void CloseButton()
    {
        tutorialUI.SetActive(false);
    }
    public virtual void NextButton()
    {
        //Xử lý việc load image or text
    }
    public virtual void BackButton()
    {
        //Xử lý việc quay lui Image or text
    }
}
