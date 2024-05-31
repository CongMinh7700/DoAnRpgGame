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
       playerCtrl.transform.position = new Vector3(103, 14, 310);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
        }
    }

  
}
