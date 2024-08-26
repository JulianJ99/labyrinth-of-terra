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
    
    public int characterPool = 3;
    public PartyManager partyManager;
    public GameObject[] characterCarrier;
    private string currentName;
    private string className;
    private int currentIndex = 0;
    private Sprite currentImage;
    private int currentCost;

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

    [Header("Traits")]
    public List<Traits> traits = new List<Traits>();
    public float randomChance = 0.15f;

    [Header("Resources")]
    public int minCost, maxCost;

    private bool alreadyGenerated = false;

    void OnEnable()
    {
        if(!alreadyGenerated)
        {
            GenerateRandomCharacters();
            alreadyGenerated = true;
        }
    }

    private void GenerateRandomCharacters()
    {
        for(int i = 0; i < characterPool; i++)
        {
            var HeroPrefab = Instantiate(heroFramework.gameObject);
            CharacterClasses classManager = HeroPrefab.GetComponent<CharacterClasses>();

            classManager.RandomizeClass();
            classManager.AdjustStatsGrowths();
            classManager.ChangeCharacter();
            classManager.AssignAffinities();
            classManager.GiveWeapon();
            classManager.SetSkills();

            RandomizeTraits(HeroPrefab);

            currentImage = HeroPrefab.GetComponent<SpriteRenderer>().sprite;
            RandomizeValues();
            ChangeCharacterStats(HeroPrefab);
  
            Button button = characterCarrier[i].GetComponentInChildren<Button>();
            button.onClick.AddListener(() => partyManager.RecruitCharacter(HeroPrefab, button, currentCost));

            HeroPrefab.name = currentName;
            currentIndex++;
        }
    }

    private void ChangeCharacterStats(GameObject HeroToChange)
    {
        Hero1 currentChar = HeroToChange.GetComponent<Hero1>();
        currentChar.UnitName = currentName;
        className = currentChar.className;

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

        currentChar.cost = UnityEngine.Random.Range(minCost, maxCost);

        currentCost = currentChar.cost;

        SelectUI(currentChar);
    }

    private void SelectUI(Hero1 character)
    {
        CharacterUI ui = characterCarrier[currentIndex].GetComponent<CharacterUI>();

        ui.characterName.text = character.UnitName;
        ui.characterImage.sprite = character.gameObject.GetComponent<SpriteRenderer>().sprite;
        ui.characterClass.text = character.className;
        ui.characterLevel.text = character.currentLevel.ToString();
        ui.characterHealth.text = character.health.ToString();
        ui.characterStrength.text = character.strength.ToString();
        ui.characterDexterity.text = character.dexterity.ToString();
        ui.characterResistance.text = character.resistance.ToString();
        ui.characterExperience.text = character.expAmount.ToString();
        ui.characterMovement.text = character.movementRange.ToString();
        ui.characterMagic.text = character.magic.ToString();
        ui.characterSpeed.text = character.speed.ToString();
        ui.characterLuck.text = character.luck.ToString();
        ui.characterCost.text = character.cost.ToString() + " Buy";
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

    private void RandomizeTraits(GameObject currentCharacter)
    {
        TraitManager manager = currentCharacter.GetComponent<TraitManager>();
        if(UnityEngine.Random.value < randomChance)
        {
            int randomValue = UnityEngine.Random.Range(0, traits.Count);
            manager.AddTrait(traits[randomValue]);
        }
    }
}
