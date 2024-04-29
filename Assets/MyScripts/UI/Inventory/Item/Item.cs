using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "SO/Item")]
public class Item : ScriptableObject
{
    public ItemType type;
    public string itemName;
    public int itemPerSlot;
    public Sprite icon;
    public GameObject prefab;

    [TextArea]
    public string itemInformation;
}
