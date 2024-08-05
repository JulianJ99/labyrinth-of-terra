using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public ResourceManager ResourceManager;
    public List<GameObject> partyMembers = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void RecruitCharacter(GameObject character, Button button, int amount)
    {
        int myMoney = ResourceManager.CheckCurrent();

        if(myMoney > amount)
        {
            partyMembers.Add(character);
            button.interactable = false;
            ResourceManager.SubtractValue(amount);

            character.transform.parent = transform;
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
