using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : RPGMonoBehaviour
{
    [SerializeField] protected GameObject winScreen;
    [SerializeField] protected TextMeshProUGUI completeText;
    [SerializeField] protected SettingCanvas settingCanvas;
    public bool isAnimatingText = false;
    private Coroutine textAnimationCoroutine;
    protected override void LoadComponents()
    {
        LoadWinScreen();
        LoadText();
        LoadSettingCanvas();
    }
    protected virtual void LoadWinScreen()
    {
        if (this.winScreen != null) return;
        this.winScreen = this.gameObject;
    }
    protected virtual void LoadSettingCanvas()
    {
        if (this.settingCanvas != null) return;
        this.settingCanvas = GetComponentInParent<SettingCanvas>();
    }
    protected virtual void LoadText()
    {
        if (this.completeText != null) return;
        this.completeText = GetComponentInChildren<TextMeshProUGUI>();
        completeText.text = "";
    }
    protected override void OnEnable()
    {
        string text = " Bạn đã hoàn thành tất cả các nhiệm vụ." +
           "\n Cảm ơn bạn đã dành thời gian quý báu của mình để trải nghiệm game." +
           "\n\nMade by Công Minh < 3";
        StartCoroutine(AnimateText(text));
    }
    IEnumerator AnimateText(string fullText)
    {
        yield return new WaitForSeconds(1f);
        isAnimatingText = true;
        for (int i = 0; i <= fullText.Length; i++)
        {
           completeText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        isAnimatingText = false;
        winScreen.SetActive(false);
        settingCanvas.isShow = true;
    }
}
