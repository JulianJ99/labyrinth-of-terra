using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapons weaponRef;
    private SpriteRenderer spriteRenderer;

    public string type;
    public int durability;
    public bool isBroken = false;
    public float hitChance;
    public float critChance;
    public float requiredAffinity;
    public float cost;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetWeapon();
    }

    public void SetWeapon()
    {
        durability = weaponRef.weaponDurability;
        spriteRenderer.sprite = weaponRef.weaponSprite;
        gameObject.name = weaponRef.weaponName;
        type = weaponRef.weaponType;
        hitChance = weaponRef.weaponHit;
        critChance = weaponRef.weaponCrit;
        requiredAffinity = weaponRef.weaponReqAff;

        cost = weaponRef.weaponCost;
    }
}
