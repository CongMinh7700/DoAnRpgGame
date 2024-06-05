using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class VillagePortal : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected SaveScripts saveScripts;
    [SerializeField] protected GameObject villagePortal;
    protected override void LoadComponents()
    {
        LoadPlayerCtrl();
        LoadSaveScripts();
    }
    protected virtual void LoadSaveScripts()
    {
        if (saveScripts != null) return;
        this.saveScripts = FindObjectOfType<SaveScripts>(); ;
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    protected  void Start()
    {
       //playerCtrl.transform.position = new Vector3(103, 14, 310);
       //SaveGame.Instance.SaveScripts.SaveData("");
      // SaveGame.Instance.Load();
    }
   
    //nếu có bug thì cho về cái ban đầu
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCtrl.transform.position = new Vector3(62, 27, 302);
            SaveGame.Instance.SaveScripts.SaveData("");
            SceneManager.LoadScene(3);
        }
    }

  
}
