using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    [SerializeField] protected static FXSpawner instance;
    public static FXSpawner Instance => instance;

    public static string heal = "Heal";
    public static string strength = "Strength";
    public static string shield = "Shield";
    public static string notification = "Notification";


    protected override void Awake()
    {
        if (FXSpawner.instance != null) Debug.LogWarning("Only 1 FX Spawner allow to exist");
        FXSpawner.instance = this;

    }
}
