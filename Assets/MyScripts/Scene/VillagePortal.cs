using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class VillagePortal : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    protected override void LoadComponents()
    {
        LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    private void Start()
    {
        if (playerCtrl.usingPortal)
        {
            playerCtrl.transform.position = new Vector3(103, 14, 310);
            StartCoroutine(WaitToFalse());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
            playerCtrl.usingPortal = true;
        }
    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(2f);
        playerCtrl.usingPortal = false;
    }
  
}
