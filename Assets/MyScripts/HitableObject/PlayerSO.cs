using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObject", menuName = "SO/PlayerObject")]
public class PlayerSO : HitableObjectSO
{
    public int damage = 2;
    public int stamina = 2;
    public int mana = 2;
    public int defense = 2;
}
