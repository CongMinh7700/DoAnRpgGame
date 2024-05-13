using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSpawner : Spawner
{
    [SerializeField] protected static FxSpawner instance;
    public static FxSpawner Instance => instance;

    public static string heal = "Heal";


    protected override void Awake()
    {
        if (FxSpawner.instance != null) Debug.LogWarning("Only 1 FX Spawner allow to exist");
        FxSpawner.instance = this;

    }
}
