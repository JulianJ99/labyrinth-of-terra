using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public new List<GameObject> inventory = new List<GameObject>();
    public int inventorySpace;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddToInventory(GameObject item)
    {
        if(inventory.Count <= inventorySpace)
        {
            inventory.Add(item);
        }
        else
        {
            FullInventory();
        }
    }

    public void RemoveFromInventory(GameObject item)
    {

    }

    public void Equipitem(GameObject item)
    {

    }

    public void UseItem(GameObject item)
    {

    }

    public void FullInventory()
    {
        print("testing");
    }
}
