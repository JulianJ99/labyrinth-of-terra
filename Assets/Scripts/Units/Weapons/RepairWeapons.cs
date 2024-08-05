using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairWeapons : MonoBehaviour
{
    public WeaponManager weaponToRepair;
    public ResourceManager manager;

    public Button repairButton;
    public TMP_Text weaponName, repairCostText;
    public Image weaponSprite;

    public bool weaponInserted = false;

    public int baseRepairCost = 100;
    private int repairCost;

    public void Update()
    {
        if(weaponInserted)
        {
            weaponName.text = weaponToRepair.name;
            weaponSprite.sprite = weaponToRepair.GetComponent<SpriteRenderer>().sprite;
            repairCost = CalculateRepairCost();
            repairCostText.text = repairCost.ToString();
        }
    }

    public void RepairWeapon()
    {
        int myMoney = manager.CheckCurrent();

        if(myMoney > repairCost)
        {
            manager.SubtractValue(repairCost);
            weaponToRepair.durability = weaponToRepair.maxDurability;
        }
    }

    private int CalculateRepairCost()
    {
        int currentDurability = weaponToRepair.durability;
        int maxDurability = weaponToRepair.maxDurability;

        float missingDurability = (maxDurability - currentDurability) / (float)maxDurability;

        int repairCost = Mathf.CeilToInt(baseRepairCost * missingDurability);

        return repairCost;
    }
}
