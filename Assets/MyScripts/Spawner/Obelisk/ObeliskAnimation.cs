using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskAnimation : RPGMonoBehaviour
{
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected SpawnRandom spawnRandom;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject effect;
    public bool canAnimation;
    protected override void LoadComponents()
    {
        LoadSpawnerCtrl();
        LoadAnimator();
        LoadSpawnerRandom();
    }
    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponentInParent<SpawnerCtrl>();
    }
    protected virtual void LoadSpawnerRandom()
    {
        if (this.spawnRandom != null) return;
        this.spawnRandom = GetComponentInParent<SpawnRandom>();
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }
    private void Start()
    {
        effect.SetActive(false);
        canAnimation = true;
    }
    private void Update()
    {
        if (this.spawnerCtrl.SpawnTrigger.CanSpawn)
        {
            animator.SetBool("Spawn",canAnimation);
            StartCoroutine(WaitToPlay());
        }
    }
    IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(7f);
        effect.SetActive(true);
        canAnimation = false;
        yield return new WaitForSeconds(2f);
        effect.SetActive(false);

    }
}
