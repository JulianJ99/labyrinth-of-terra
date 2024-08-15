using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terra {
public class GlobalInventory : MonoBehaviour
{
    public List<GameObject> globalInventory = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddToConvoy(GameObject item)
    {
        globalInventory.Add(item);
        item.transform.parent = transform;
    }

    public void TakeFromConvoy(GameObject item)
    {

    }

    public void RemoveFromConvoy(GameObject item)
    {

    }
}
}