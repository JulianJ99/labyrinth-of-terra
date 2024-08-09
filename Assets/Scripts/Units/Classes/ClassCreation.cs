using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Classes")]
public class ClassCreation : ScriptableObject
{
    public string className;
    public Sprite classSprite;
    public GameObject classWeapon;

    [Range(0f, 1f)] public float hpvPrio;
    [Range(0f, 1f)] public float atkPrio;
    [Range(0f, 1f)] public float defPrio;
    [Range(0f, 1f)] public float movPrio;
    [Range(0f, 1f)] public float strPrio;
    [Range(0f, 1f)] public float magPrio;
    [Range(0f, 1f)] public float dexPrio;
    [Range(0f, 1f)] public float spdPrio;
    [Range(0f, 1f)] public float resPrio;
    [Range(0f, 1f)] public float lckPrio;

    public List<WeaponAffinity> weaponAffinities;
    public List<Skills> classSkills;
}

