using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Terra { 
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("References")]
    public BaseEnemy enemyFramework;
    public int enemyPool = 3;
    

    private string currentName;
    private string className;
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

    [Header("Traits")]
    public List<Traits> traits = new List<Traits>();
    public float randomChance = 0.15f;

    [Header("List")]
    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        GenerateRandomEnemies();
    }


    private void GenerateRandomEnemies()
    {
        for(int i = 0; i < enemyPool; i++)
        {
            Debug.Log("Making enemies");
            var enemyPrefab = Instantiate(enemyFramework.gameObject);
            CharacterClasses classManager = enemyPrefab.GetComponent<CharacterClasses>();

            classManager.RandomizeClass();
            classManager.AdjustStatsGrowths();
            classManager.ChangeCharacter();
            classManager.AssignAffinities();
            classManager.GiveWeapon();
            classManager.SetSkills();

            RandomizeTraits(enemyPrefab);

            currentImage = enemyPrefab.GetComponent<SpriteRenderer>().sprite;
            RandomizeValues();
            ChangeCharacterStats(enemyPrefab);

            enemyPrefab.name = currentName;
            enemyList.Add(enemyPrefab);
            enemyPrefab.transform.parent = transform;
            
        }
    }

    private void ChangeCharacterStats(GameObject EnemyToChange)
    {
        Enemy1 currentChar = EnemyToChange.GetComponent<Enemy1>();
        currentChar.UnitName = currentName;
        className = currentChar.className;

        currentChar.health = RandomizeValue(hpvMin, hpvMax, currentChar.hpvPrio);
        currentChar.movementRange = RandomizeValue(movMin, movMax, currentChar.movPrio);
        currentChar.strength = RandomizeValue(strMin, strMax, currentChar.strPrio);
        currentChar.defense = RandomizeValue(defMin, defMax, currentChar.defPrio);
        currentChar.magic = RandomizeValue(magMin, magMax, currentChar.magPrio);
        currentChar.dexterity = RandomizeValue(dexMin, dexMax, currentChar.dexPrio);
        currentChar.speed = RandomizeValue(spdMin, spdMax, currentChar.spdPrio);
        currentChar.resistance = RandomizeValue(resMin, resMax, currentChar.resPrio);
        currentChar.luck = RandomizeValue(lckMin, lckMax, currentChar.lckPrio);

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
}
