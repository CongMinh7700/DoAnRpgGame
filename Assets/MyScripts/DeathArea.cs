using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class DeathArea : RPGMonoBehaviour
{
    [SerializeField] protected BoxCollider boxCollider;
    protected override void LoadComponents()
    {
        LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamageReceiver.isDeath = true;
            StartCoroutine(WaitToLoad());
        }
    }
   
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1.5f);
        SaveGame.Instance.DeleteData();
        LevelSystem.Instance.bossKill = 0;
        yield return new WaitForSeconds(1.5f);
        PlayerDamageReceiver.isDeath = false;
        SceneManager.LoadScene(0);
    }
}
