using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventory : MonoBehaviour
{
    public new List<GameObject> globalInventory = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToConvoy(GameObject item)
    {
        globalInventory.Add(item);
    }

    public void TakeFromConvoy(GameObject item)
    {

    }

    public void RemoveFromConvoy(GameObject item)
    {

    }
}
