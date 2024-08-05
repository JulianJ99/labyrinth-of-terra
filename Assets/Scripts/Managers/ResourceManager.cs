using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int currentMoney;
    public TMP_Text moneyDisplay;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SubtractValue(int amount)
    {
        currentMoney -= amount;
        moneyDisplay.text = currentMoney.ToString();
    }

    public int CheckCurrent()
    {
        return currentMoney;
    }

    public void AddValue(int amount)
    {
        currentMoney += amount;
    }
}
