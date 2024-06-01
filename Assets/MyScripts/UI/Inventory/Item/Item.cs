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
    public bool isFood;
    [TextArea(5,5)]
    public string itemInformation;
    public List<BonusAttribute> bonusAttributes = new List<BonusAttribute>();

}
[System.Serializable]
public class BonusAttribute
{
    public string attributeName;
    public int attributeValue;
}
