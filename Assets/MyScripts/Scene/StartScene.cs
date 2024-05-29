using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    [SerializeField] protected TMP_InputField nameInput ;
    [SerializeField] protected GameObject tutorialUI;

    private void Start()
    {
        //tutorialUI.SetActive(false);
    }
    public void StartButton()
   {
        SFXManager.Instance.PlaySFXClick();
        //ResetQuest
        //Không load game = new Game
        Debug.Log("Name player is : " + nameInput.text);
        PlayerInfoManager.playerNameData = nameInput.text;
        Debug.Log("Main Story Scene Start"); 
        SceneManager.LoadScene(1);
    }
    public void ContinueButton()
    {
        SFXManager.Instance.PlaySFXClick();
        //chỉnh lại thành secneIndex khi nếu qua scene 2 thì load scene2
        //Savegame.LoadData();
        //có thể không gọi usingPortal
        SceneManager.LoadScene(3);
    }
    public void TutorialButton()
    {
       // tutorialUI.gameObject.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
