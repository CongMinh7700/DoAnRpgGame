using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HitableObject", menuName = "SO/HitableObject")]
public class HitableObjectSO : ScriptableObject
{
    public string objName = "HitableObject";
    public ObjectType objType;
    public int hpMax = 2;
    public int damage = 2;
    public int defense = 2;
    // public List<ItemDropRate> dropList;
}
