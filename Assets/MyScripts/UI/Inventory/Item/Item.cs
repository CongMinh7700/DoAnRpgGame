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
    //public int healthBonus;
    //public int restoreBonus;
    //public int damageBonus;
    //public int defendBonus;
    //public int speedBonus;
    [TextArea]
    public string itemInformation;
    public List<BonusAttribute> bonusAttributes = new List<BonusAttribute>();

}
[System.Serializable]
public class BonusAttribute
{
    public string attributeName;
    public int attributeValue;
}
