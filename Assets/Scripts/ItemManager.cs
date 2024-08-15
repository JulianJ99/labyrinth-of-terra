using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terra {
public class ItemManager : MonoBehaviour
{

    public Items[] itemReferences;
    public Items itemRef;

    public Skills attachedSkill;
    public string itemType;

    public int uses;
    public int healingValue;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetItem();
    }

    public void SetItem()
    {
        if(itemRef != null)
        {
            spriteRenderer.sprite = itemRef.itemSprite;
            gameObject.name = itemRef.itemName;

            uses = itemRef.itemUses;
            healingValue = itemRef.itemHeal;
        }
        else
        {
            print("No ref");
        }
    }

    public void UseItem(GameObject character)
    {
        if(itemType == "Potion")
        {
            Hero1 unit = character.GetComponent<Hero1>();
            unit.HealUnit(healingValue);
        }

        if(itemType == "Skillbook")
        {
            SkillManager skills = character.GetComponent<SkillManager>();
            skills.AddSkill(attachedSkill);
        }

        --uses;

        if (uses == 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetReference(Skills skill)
    {
        attachedSkill = skill;

        if(skill.SkillRarity == "Rare")
        {
            itemRef = itemReferences[0];
        }
        else if(skill.SkillRarity == "Epic")
        {
            itemRef = itemReferences[1];
        }
        else if(skill.SkillRarity == "Legendary")
        {
            itemRef = itemReferences[2];
        }
        else if(skill.SkillRarity == "Divine")
        {
            itemRef = itemReferences[3];
        }
    }
}
}
