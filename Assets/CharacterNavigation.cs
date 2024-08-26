using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigation : MonoBehaviour
{
    public GameObject[] characterCarrier;

    // Start is called before the first frame update
    void OnEnable() 
    {
        foreach (GameObject character in characterCarrier)
        {
            character.SetActive(false);
        }

        characterCarrier[0].SetActive(true);
    }

    public void SwitchCharacter(int identifier)
    {
        if(identifier == 0)
        {
            foreach(GameObject character in characterCarrier)
            {
                character.SetActive(false);
            }

            characterCarrier[0].SetActive(true);
        }
        if(identifier == 1)
        {
            foreach (GameObject character in characterCarrier)
            {
                character.SetActive(false);
            }

            characterCarrier[1].SetActive(true);
        }
        if (identifier == 2)
        {
            foreach (GameObject character in characterCarrier)
            {
                character.SetActive(false);
            }

            characterCarrier[2].SetActive(true);
        }
    }
}
