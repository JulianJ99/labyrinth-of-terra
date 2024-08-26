using System.Collections;
using System.Collections.Generic;
using Terra;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour
{
    public GameObject[] characterSlots;

    private PartyCharacter character;
    private int partyCount;
    // Start is called before the first frame update
    void OnEnable()
    {
        partyCount = PartyManager.Instance.partyMembers.Count;

        for (int i = 0; i < partyCount; i++)
        {
            characterSlots[i].SetActive(true);
            character = characterSlots[i].GetComponent<PartyCharacter>();
            character.linkedInventory = PartyManager.Instance.partyMembers[i].GetComponent<CharacterInventory>();
            character.text.text = PartyManager.Instance.partyMembers[i].GetComponent<Hero1>().UnitName;
            character.image.sprite = PartyManager.Instance.partyMembers[i].GetComponent<SpriteRenderer>().sprite; 
        }

        for (int j = partyCount; j < characterSlots.Length; j++)
        {
            characterSlots[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
