using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    //[SerializeField] protected InputField a ;
    [SerializeField] protected TMP_InputField nameInput ;

   public void NextScene()
    {
        Debug.Log("Name player is : " + nameInput.text);
        PlayerInfoManager.playerNameData = nameInput.text;
        Debug.Log("Main Story Scene Start"); 
        SceneManager.LoadScene(1);
    }
}
