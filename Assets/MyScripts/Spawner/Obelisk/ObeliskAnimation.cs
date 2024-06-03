using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskAnimation : RPGMonoBehaviour
{
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected SpawnRandom spawnRandom;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject effect;
    //[SerializeField] protected GameObject fences;
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
        this.spawnerCtrl = GetComponentInChildren<SpawnerCtrl>();
    }
    protected virtual void LoadSpawnerRandom()
    {
        if (this.spawnRandom != null) return;
        this.spawnRandom = GetComponentInChildren<SpawnRandom>();
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
        if (this.spawnerCtrl.SpawnTrigger.CanSpawn && !spawnRandom.bossSpawned && spawnerCtrl.canSpawnBoss)
        {
            animator.SetBool("Spawn",canAnimation);
            StartCoroutine(WaitToPlay());
        }
    }
    IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(7f);
        if(canAnimation) effect.SetActive(true);
        canAnimation = false;
        yield return new WaitForSeconds(2f);
        effect.SetActive(false);

    }
}
