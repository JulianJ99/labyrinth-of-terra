using System.Collections;
using System.Collections.Generic;
using Terra;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyCharacter : MonoBehaviour
{
    public CharacterInventory linkedInventory;
    public TMP_Text text;
    public Image image;

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
                weapon.characterLinked = true;
                weapon.characterInventory = linkedInventory;
            }
        }
        if (skillOverview)
        {
            skills.characterLinked = true;
            skills.characterInventory = linkedInventory;
        }
    }
}
