using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public Items[] itemReferences;
    public Items itemRef;

    public Skills attachedSkill;
    public string itemType;

    public int uses;
    public float healingValue;

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
    }

    public void UseItem()
    {
        --uses;

        if(uses == 0)
        {
            Destroy(gameObject);
        }

        if(itemType == "Potion")
        {

        }

        if(itemType == "Skillbook")
        {

        }
    }

    public void SetReference(Skills skill)
    {
        attachedSkill = skill;
        print(attachedSkill);

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

        //SetItem();
    }
}
