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
}

[System.Serializable]
public class WeaponAffinity
{
    public Weapons weapon;
    [Range(0f, 100f)] public float affinity;
}
