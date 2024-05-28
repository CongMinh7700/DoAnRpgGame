using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingCanvas : RPGMonoBehaviour
{
    [SerializeField] protected GameObject settingUI;
    [SerializeField] protected GameObject pauseUI;

    private void Start()
    {
        settingUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    protected override void LoadComponents()
    {
        LoadPauseUI();
    }
    protected virtual void LoadPauseUI()
    {
        if (this.pauseUI != null) return;
        this.pauseUI = this.gameObject;
    }
    public virtual void SettingButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        Time.timeScale = 0;
        settingUI.SetActive(true);
    }
    public virtual void SaveButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        SaveNotification();
        SaveGame.Instance.Save();
    }
    public virtual void HomeButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        SceneManager.LoadScene(0);
    }
    public virtual void ExitButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        SaveGame.Instance.Save();
        Application.Quit();
    }
    public virtual void ResumeButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }
    public virtual void OptionButton()
    {
        AudioClickManager.Instance.PlaySFXClick();
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    protected virtual void SaveNotification()
    {
        string fxName = FXSpawner.notification;
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        NotificationText nofText = fxObj.GetComponentInChildren<NotificationText>();
        nofText.SetText("Lưu game thành công");
        fxObj.gameObject.SetActive(true);
    }
}
