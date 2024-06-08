using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : RPGMonoBehaviour
{
    [SerializeField] protected TMP_InputField nameInput ;
    [SerializeField] protected GameObject tutorialUI;
    [SerializeField] protected GameObject objectShow;
    public static int sceneIndex = 1;
    private void Start()
    {
        tutorialUI.SetActive(false);
    }
    public void StartButton()
   {
        SFXManager.Instance.PlaySFXClick();
        //ResetQuest
        //Không load game = new Game
        Debug.Log("Name player is : " + nameInput.text);
        if(nameInput.text == "")
        {
            PlayerInfoManager.playerNameData = "Minh";
        }
        else
        {
            PlayerInfoManager.playerNameData = nameInput.text;
        }
   
        Debug.Log("Main Story Scene Start"); 
        SceneManager.LoadScene(1);
        SaveGame.newGame = true;
    }
    public void ContinueButton()
    {
        if (SaveGame.HasData())
        {
            SFXManager.Instance.PlaySFXClick();
            //chỉnh lại thành secneIndex khi nếu qua scene 2 thì load scene2
            //Savegame.LoadData();
            //có thể không gọi usingPortal
            SaveGame.newGame = false;
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("SceneIndex : " + sceneIndex);
        }
        else
        {
            Debug.Log("No data");
            objectShow.SetActive(true);
        }
       
    }
    public void TutorialButton()
    {
        tutorialUI.gameObject.SetActive(true);
    }
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }


  

}


