using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Terra {
public class UpgradeWeapons : MonoBehaviour
{
    public WeaponManager weaponToUpgrade;
    public ResourceManager manager;

    public Button mightButton, hitButton, critButton;
    public TMP_Text weaponName, upgradeCostText;
    public Image weaponSprite;

    public bool weaponInserted = false;

    public int baseUpgradeCost;
    public int increaseCost;
    private int upgradeCost;

    public void UpgradeMight()
    {
        int myMoney = manager.CheckCurrent();

        if(myMoney > upgradeCost)
        {
            weaponToUpgrade.GetComponent<WeaponManager>().UpgradeWeaponMight();
            manager.SubtractValue(upgradeCost);
            UpdateUpgradeCost();
        }
    }
    public void UpgradeHit()
    {
        int myMoney = manager.CheckCurrent();

        if (myMoney > upgradeCost)
        {
            weaponToUpgrade.GetComponent<WeaponManager>().UpgradeWeaponHit();
            manager.SubtractValue(upgradeCost);
            UpdateUpgradeCost();
        }
    }
    public void UpgradeCrit()
    {
        int myMoney = manager.CheckCurrent();

        if (myMoney > upgradeCost)
        {
            weaponToUpgrade.GetComponent<WeaponManager>().UpgradeWeaponCrit();
            manager.SubtractValue(upgradeCost);
            UpdateUpgradeCost();
        }
    }
    public void Update()
    {
        if(weaponInserted)
        {
            weaponName.text = weaponToUpgrade.name;
            weaponSprite.sprite = weaponToUpgrade.GetComponent<SpriteRenderer>().sprite;
            upgradeCostText.text = upgradeCost.ToString();
            UpdateUpgradeCost();
        }
    }
    private void UpdateUpgradeCost()
    {
        int weaponLevel = weaponToUpgrade.GetComponent<WeaponManager>().weaponLevel;
        upgradeCost = CalculateUpgradeCost(baseUpgradeCost, weaponLevel);
    }

    private int CalculateUpgradeCost(int baseCost, int level)
    {
        return baseCost + (level * increaseCost);
    }
}
}
