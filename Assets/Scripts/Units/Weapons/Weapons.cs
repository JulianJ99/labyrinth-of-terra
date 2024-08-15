using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class Weapons : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public string weaponType;
    public string weaponSpecial;

    public int weaponDurability;
    public int weaponMight;
    public float weaponHit;
    public float weaponCrit;
    public float weaponReqAff;
    public int weaponCost;

    public int upgradeMight;
    public float upgradeHit;
    public float upgradeCrit;
}

[System.Serializable]
public class WeaponAffinity
{
    public Weapons weapon;
    [Range(0f, 100f)] public float affinity;
}
