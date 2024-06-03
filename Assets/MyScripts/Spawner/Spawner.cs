using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : RPGMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;
    [SerializeField] public int spawnedCount = 0;
    [SerializeField] protected Transform holder;

    protected override void LoadComponents()
    {
        LoadPrefabs();
        LoadHolder();
    }
    public virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach( Transform prefab in prefabObj)
        {
            prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }
    public virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.LogWarning(transform.name + "|LoadHolder|", gameObject);
    }

    public  virtual void HidePrefabs()
    {
        foreach(Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform GetRandomPrefabs()
    {
        int random = Random.Range(0, prefabs.Count);
        return prefabs[random];
    }
    protected virtual Transform GetPrefabByName(string prefabName)
    {
        foreach(Transform prefab in prefabs)
        {
            if (prefab.name == prefabName) 
                return prefab;
        }
        return null;
    }
    public virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in poolObjs)
        {
            if (poolObj == null) continue;
            if (poolObj.name == prefab.name && !poolObj.gameObject.activeSelf)
            {
                poolObjs.Remove(poolObj);
                return poolObj;
            }
        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    public virtual Transform Spawn(string prefabName ,Vector3 position ,Quaternion rotation)
    {
        Transform prefab = GetPrefabByName(prefabName);
        if(prefab == null)
        {
            Debug.Log("Prefab not found");
            return null;
        }
        return Spawn(prefab, position, rotation);
    }
    public virtual Transform Spawn(Transform prefab,Vector3 position,Quaternion rotation)
    {
        Transform newPrefab = GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(position, rotation);
        newPrefab.SetParent(this.holder);
        newPrefab.gameObject.SetActive(true);
        spawnedCount++;
        return newPrefab;
    }
    public virtual void Despawn(Transform obj)
    {
       // Debug.Log(transform.name + "Add to pool" + obj.name);
        if (this.poolObjs.Contains(obj)) return;
        obj.gameObject.SetActive(false);
        this.poolObjs.Add(obj);
        this.spawnedCount--;
    }


}
