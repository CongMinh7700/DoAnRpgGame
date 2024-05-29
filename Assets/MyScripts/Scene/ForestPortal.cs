using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestPortal : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    protected override void LoadComponents()
    {
        LoadPlayerCtrl();
    }
    private void Start()
    {
        if (!playerCtrl.usingPortal)
        {
            playerCtrl.transform.position = new Vector3(62, 27, 302);
            StartCoroutine(WaitToFalse());
        }
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            playerCtrl.usingPortal = true;
        }
    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(2f);
        playerCtrl.usingPortal = false;
    }

}
