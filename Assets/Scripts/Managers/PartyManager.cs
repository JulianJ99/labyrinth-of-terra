using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public ResourceManager ResourceManager;
    public List<GameObject> partyMembers = new List<GameObject>();

    public void RecruitCharacter(GameObject character, Button button, int amount)
    {
        int myMoney = ResourceManager.CheckCurrent();

        if(myMoney > amount)
        {
            partyMembers.Add(character);
            button.interactable = false;
            ResourceManager.SubtractValue(amount);
        }
        else
        {
            print("you poor");
        }
    }

    public void RemoveCharacter(GameObject character)
    {
        partyMembers.Remove(character);
    }
}
