using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskAnimation : RPGMonoBehaviour
{
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected SpawnRandom spawnRandom;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject effect;
    [SerializeField] protected GameObject fences;
    public static bool canAnimation;
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
        if(fences != null)
        {
            fences.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Nofences");
        }
 
    }
    private void Update()
    {
        Debug.Log("Spawn Random Spawned"+spawnRandom.bossSpawned);
        if (this.spawnerCtrl.SpawnTrigger.CanSpawn  && !spawnRandom.bossSpawned)
        {
            animator.SetBool("Spawn",canAnimation);
            StartCoroutine(WaitToPlay());
        }
        if(this.spawnerCtrl.Spawner.spawnedCount <= 0)
        {
            if(fences != null)
            {
                fences.SetActive(false);
            }
            else
            {
                Debug.LogWarning("NoFences nè cha nội");
            }

        }
    }
    IEnumerator WaitToPlay()
    {
        Debug.Log("Call Wait to play");
        yield return new WaitForSeconds(7f);
        if(canAnimation) effect.SetActive(true);
        canAnimation = false;
        if(fences != null)
        {
            fences.SetActive(true);
        }
        else
        {
            Debug.Log("NoFences");
        }

        yield return new WaitForSeconds(2f);
        effect.SetActive(false);

    }
}
