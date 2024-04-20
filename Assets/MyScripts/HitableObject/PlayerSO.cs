using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObject", menuName = "SO/PlayerObject")]
public class PlayerSO : ScriptableObject
{
    public string playerName = "Player";
    public int hpMax = 2;
}
