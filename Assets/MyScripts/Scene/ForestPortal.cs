using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestPortal : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected SaveScripts saveScripts;
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
    private void Start()
    {


        playerCtrl.transform.position = new Vector3(62, 27, 302);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }


}
