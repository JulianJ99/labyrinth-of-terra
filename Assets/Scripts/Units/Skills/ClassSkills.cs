using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terra {
[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public string SkillName;
    public string Effect;
    public int EffectNumber;
    public string StatToEffect;

    public bool SkillActive;

    [Header("Purchaseable skills")]
    public bool isPurchaseable;
    public string SkillRarity;
}
}


