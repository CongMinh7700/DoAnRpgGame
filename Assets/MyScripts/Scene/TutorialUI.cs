using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;
    public virtual void ExitButton()
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
