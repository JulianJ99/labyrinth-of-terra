using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Terra {
public class MerchantWeapons : MonoBehaviour
{
    public GameObject weapon;
    public GlobalInventory inventory;
    public CharacterInventory characterInventory;
    public bool characterLinked;
    public ResourceManager manager;

    public Image image;
    public TMP_Text nameText;
    public TMP_Text durabilityText;
    public TMP_Text costText;
    public Button button;

    private SpriteRenderer spriteRenderer;
    private int weaponCost;
    private bool alreadySet = false;

    public void OnEnable()
    {
        if(!alreadySet)
        {
            WeaponManager weaponInfo = weapon.GetComponent<WeaponManager>();
            spriteRenderer = weapon.GetComponent<SpriteRenderer>();

            image.sprite = spriteRenderer.sprite;
            nameText.text = weapon.name;
            durabilityText.text = weaponInfo.durability.ToString() + "/" + weaponInfo.durability.ToString();
            costText.text = weaponInfo.cost.ToString();
            weaponCost = weaponInfo.cost;

            button.onClick.AddListener(() => PurchaseWeapon());
            alreadySet = true;
        }
    }

    public void PurchaseWeapon()
    {
        int myMoney = manager.CheckCurrent();
        if(myMoney >= weaponCost)
        {
           GameObject weaponInstance = Instantiate(weapon);
           manager.SubtractValue(weaponCost);
            if(!characterLinked)
                {
                    inventory.AddToConvoy(weaponInstance);
                }
            else
                {
                    characterInventory.AddToInventory(weaponInstance);
                }
        }
    }
}
}