using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int currentMoney;

    public void SubtractValue(int amount)
    {
        currentMoney -= amount;
    }

    public int CheckCurrent()
    {
        return currentMoney;
    }
}
