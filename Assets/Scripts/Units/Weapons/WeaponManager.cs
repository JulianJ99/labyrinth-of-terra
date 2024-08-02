using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapons weaponRef;
    private SpriteRenderer spriteRenderer;
    public MerchantWeapons weaponsCreation;

    public int weaponLevel;
    public string type;
    public int might;
    public int durability;
    public bool isBroken = false;
    public float hitChance;
    public float critChance;
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
        durability = weaponRef.weaponDurability;
        spriteRenderer.sprite = weaponRef.weaponSprite;
        gameObject.name = weaponRef.weaponName;
        type = weaponRef.weaponType;
        hitChance = weaponRef.weaponHit;
        critChance = weaponRef.weaponCrit;
        requiredAffinity = weaponRef.weaponReqAff;

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
