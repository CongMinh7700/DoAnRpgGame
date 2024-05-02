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
    //public int effectValue;
    public List<ItemEffect> itemEffects;
    public bool isFood;
    [TextArea]
    public string itemInformation;

    public void ApplyEffects(PlayerCtrl player, int value)
    {
        foreach (ItemEffect effect in itemEffects)
        {
            effect.ApplyEffect(player);
        }
    }

}
public class HealthEffect : ItemEffect
{
    public int healthValue;

    public override void ApplyEffect(PlayerCtrl player)
    {
        //player.damageReceiver.hp += healthValue;
    }
}

public class DamageEffect : ItemEffect
{
    public int damageValue;

    public override void ApplyEffect(PlayerCtrl player)
    {
       // player.damage += damageValue;
    }
}

// và các lớp khác nếu cần thiết...

