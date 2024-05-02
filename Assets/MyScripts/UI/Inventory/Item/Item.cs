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
    public GameObject prefab;//xử lý drop sau
    public int effectValue;
    public bool isFood;
    [TextArea]
    public string itemInformation;
}
