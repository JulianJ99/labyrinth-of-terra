using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("References")]
    public BaseHero heroFramework;
    public GameObject[] charSpacesUI;
    public int characterPool = 3;

    private string currentName;
    private string currentClass;
    private int currentIndex = 0;
    private Sprite currentImage;

    [Header("Random Values")]
    public List<string> randomNames = new List<string>();
    public int hpvMin, hpvMax;
    public int atkMin, atkMax;
    public int defMin, defMax;
    public int movMin, movMax;
    public int strMin, strMax;
    public int magMin, magMax; 
    public int dexMin, dexMax;
    public int spdMin, spdMax;
    public int resMin, resMax;
    public int lckMin, lckMax;

    void Start()
    {
        GenerateRandomCharacters();
    }

    private void GenerateRandomCharacters()
    {
        for(int i = 0; i < characterPool; i++)
        {
            var HeroPrefab = Instantiate(heroFramework.gameObject);
            currentImage = HeroPrefab.GetComponent<SpriteRenderer>().sprite;
            RandomizeValues();
            SelectUI();
            ChangeCharacterStats(HeroPrefab);
            currentIndex++;

            HeroPrefab.name = currentName;
        }
    }

    private void ChangeCharacterStats(GameObject HeroToChange)
    {
        Hero1 currentChar = HeroToChange.GetComponent<Hero1>();
        currentChar.UnitName = currentName;

        currentChar.health = RandomizeValue(hpvMin, hpvMax, currentChar.hpvPrio);
        currentChar.attack = RandomizeValue(atkMin, atkMax, currentChar.atkPrio);
        currentChar.defense = RandomizeValue(defMin, defMax, currentChar.defPrio);
        currentChar.movementRange = RandomizeValue(movMin, movMax, currentChar.movPrio);
        currentChar.strength = RandomizeValue(strMin, strMax, currentChar.strPrio);
        currentChar.magic = RandomizeValue(magMin, magMax, currentChar.magPrio);
        currentChar.dexterity = RandomizeValue(dexMin, dexMax, currentChar.dexPrio);
        currentChar.speed = RandomizeValue(spdMin, spdMax, currentChar.spdPrio);
        currentChar.resistance = RandomizeValue(resMin, resMax, currentChar.resPrio);
        currentChar.luck = RandomizeValue(lckMin, lckMax, currentChar.lckPrio);
    }

    private void SelectUI()
    {
        TMP_Text CharName = charSpacesUI[currentIndex].GetComponentInChildren<TMP_Text>();
        Image CharImage = charSpacesUI[currentIndex].GetComponentInChildren<Image>();
        CharName.text = currentName;
        CharImage.sprite = currentImage;
    }

    private void RandomizeValues()
    {
        currentName = randomNames[UnityEngine.Random.Range(0, randomNames.Count)];
        randomNames.Remove(currentName);
    }

    private int RandomizeValue(int minValue, int maxValue, float biasValue)
    {
        float randomValue = UnityEngine.Random.Range(minValue, maxValue);
        float biasNumber = Mathf.Lerp(minValue, maxValue, biasValue);
        return Mathf.RoundToInt((randomValue + biasNumber) / 2);

    }
}
