using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillInfo
{
    public string skillName;
    public float cooldownTime;
    public float durationTime;
    public SkillInfo(string name, float cooldown, float duration)
    {
        skillName = name;
        cooldownTime = cooldown;
        durationTime = duration;
    }
}
