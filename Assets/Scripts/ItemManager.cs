using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public Items itemRef;

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
        spriteRenderer.sprite = itemRef.itemSprite;
        gameObject.name = itemRef.itemName;

        uses = itemRef.itemUses;
        healingValue = itemRef.itemHeal;
    }
}
