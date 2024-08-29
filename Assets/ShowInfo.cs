using System.Collections;
using System.Collections.Generic;
using Terra;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowInfo : MonoBehaviour
{
    [Header("Weapons")]
    public TMP_Text nameField; 
    public TMP_Text levelField;
    public TMP_Text mtField;
    public TMP_Text hitField;
    public TMP_Text critField;
    public TMP_Text durField;
    public TMP_Text affField;

    [Header("Skills")]
    public TMP_Text skillName;
    public TMP_Text skillDesc;

    public Image imageField;
    public void DisplayWeapon(GameObject obj)
    {
        WeaponManager weapon = obj.GetComponentInParent<MerchantWeapons>().weapon.GetComponent<WeaponManager>();
        nameField.text = weapon.name;
        levelField.text = weapon.weaponLevel.ToString();
        mtField.text = weapon.might.ToString();
        hitField.text = weapon.hitChance.ToString();
        critField.text = weapon.critChance.ToString();
        durField.text = weapon.durability.ToString() + "/" + weapon.maxDurability.ToString();
        affField.text = weapon.requiredAffinity.ToString();
        imageField.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
    }
    public void DisplaySkill(GameObject obj)
    {
        ItemManager skill = obj.GetComponentInParent<SkillCarrier>().connectedSkill.GetComponent<ItemManager>();
        skillName.text = skill.attachedSkill.SkillName;
        skillDesc.text = skill.attachedSkill.Effect;
        imageField.sprite = obj.GetComponentInParent<SkillCarrier>().connectedSkill.GetComponent<SpriteRenderer>().sprite;
    }
}
