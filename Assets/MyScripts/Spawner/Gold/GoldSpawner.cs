using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : Spawner
{
    [SerializeField] protected static GoldSpawner instance;
    public static GoldSpawner Instance => instance;

    public static string gold = "Gold";

    protected override void Awake()
    {
        if (GoldSpawner.instance != null) Debug.LogWarning("Only 1 GoldSpawner allow to exist");
        GoldSpawner.instance = this;

    }
}
