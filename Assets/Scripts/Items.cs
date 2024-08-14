using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string itemType;

    public Skills attachedSkill;

    public int itemUses;
    public int itemHeal;

}
