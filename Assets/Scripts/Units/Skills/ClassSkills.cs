using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public string SkillName;
    public string Effect;
    public int EffectNumber;
    public string StatToEffect;

    public bool SkillActive;
}


