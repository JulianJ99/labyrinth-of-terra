using System.Collections;
using System.Collections.Generic;
using Terra;
using UnityEngine;

public class ConvoySelect : MonoBehaviour
{
    public bool weaponOverview = false;
    public bool skillOverview = false;
    public MerchantWeapons[] weapons;
    public ArcanistSkills skills;

    public void ChangeLinkedInventory()
    {
        if (weaponOverview)
        {
            foreach (var weapon in weapons)
            {
                weapon.characterLinked = false;
            }
        }
        if (skillOverview)
        {
            skills.characterLinked = false;
        }
    }
}
