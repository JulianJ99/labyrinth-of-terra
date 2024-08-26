using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terra {
public class WeaponManager : MonoBehaviour
{
    public Weapons weaponRef;
    private SpriteRenderer spriteRenderer;

    public int weaponLevel;
    public string type;
    public int might;
    public string damageType;
    public int durability;
    public int maxDurability;
    public bool isBroken = false;
    public float hitChance;
    public float critChance;
    public int range;
    public float requiredAffinity;
    public int cost;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetWeapon();
    }

    public void SetWeapon()
    {
        might = weaponRef.weaponMight;
        damageType = weaponRef.damageType.ToString();
        durability = weaponRef.weaponDurability;
        spriteRenderer.sprite = weaponRef.weaponSprite;
        gameObject.name = weaponRef.weaponName;
        type = weaponRef.weaponType;
        hitChance = weaponRef.weaponHit;
        critChance = weaponRef.weaponCrit;
        range = weaponRef.weaponRange;
        requiredAffinity = weaponRef.weaponReqAff;
        maxDurability = durability;

        cost = weaponRef.weaponCost;
    }

    public void UpgradeWeaponMight()
    {
        might += weaponRef.upgradeMight;
        weaponLevel++;
    }

    public void UpgradeWeaponHit()
    {
        hitChance += weaponRef.upgradeHit;
        weaponLevel++;
    }

    public void UpgradeWeaponCrit()
    {
        critChance += weaponRef.upgradeCrit;
        weaponLevel++;
    }
}
}
