using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : RPGMonoBehaviour
{
    public Animator animator;
    protected override void LoadComponents()
    {
        LoadAnimator();
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (PlayerDamageReceiver.isDeath)
        {
            animator.SetTrigger("Death");
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
