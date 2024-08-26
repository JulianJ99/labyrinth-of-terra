using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArcanistSkills : MonoBehaviour
{
    [Header("References")]
    public List<Skills> rareSkills;
    public List<Skills> epicSkills;
    public List<Skills> legendarySkills;
    public List<Skills> divineSkills;
    public GameObject[] uiSpaces;
    public GameObject skillPrefab;
    public int skillsToCreate;
    public ResourceManager manager;
    public GlobalInventory inventory;

    [Header("CostValues")]
    public int rareMin, rareMax;
    public int epicMin, epicMax;
    public int legendaryMin, legendaryMax;
    public int divineMin, divineMax;

    private Skills currentSkill;
    private bool isCreated = false;
    private int currentIndex;

    private float epicChance = 0.15f;
    private float legendaryChance = 0.10f;
    private float divineChance = 0.05f;

    private int minCost, maxCost;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!isCreated)
        {
            GenerateSkills();
        }
        isCreated = true;
    }

    private void GenerateSkills()
    {
        for (int i = 0; i < skillsToCreate; i++)
        {
            var createdSkill = Instantiate(skillPrefab);
            ItemManager itemM = createdSkill.GetComponent<ItemManager>();

            currentSkill = SelectRandomSkill();
            itemM.SetReference(currentSkill);

            int itemValue = UnityEngine.Random.Range(minCost, maxCost);

            SelectUI(itemM, itemValue);
            SetButton(createdSkill, itemValue);

            currentIndex++;
        }
    }

    private Skills SelectRandomSkill()
    {
        float random = UnityEngine.Random.value;

        if(random < divineChance)
        {
            int index = UnityEngine.Random.Range(0, divineSkills.Count);
            Skills selectedSkill = divineSkills[index];
            divineSkills.RemoveAt(index);
            minCost = divineMin; maxCost = divineMax;
            return selectedSkill;
        }
        else if(random < divineChance + legendaryChance)
        {
            int index = UnityEngine.Random.Range(0, legendarySkills.Count);
            Skills selectedSkill = legendarySkills[index];
            legendarySkills.RemoveAt(index);
            minCost = legendaryMin; maxCost = legendaryMax;
            return selectedSkill;
        }
        else if(random < divineChance + legendaryChance + epicChance)
        {
            int index = UnityEngine.Random.Range(0, epicSkills.Count);
            Skills selectedSkill = epicSkills[index];
            epicSkills.RemoveAt(index);
            minCost = epicMin; maxCost = epicMax;
            return selectedSkill;
        }
        else
        {
            int index = UnityEngine.Random.Range(0, rareSkills.Count);
            Skills selectedSkill = rareSkills[index];
            rareSkills.RemoveAt(index);
            minCost = rareMin; maxCost = rareMax;
            return selectedSkill;
        }
    }

    private void SelectUI(ItemManager item, int cost)
    {
        TMP_Text[] texts = uiSpaces[currentIndex].GetComponentsInChildren<TMP_Text>();
        Image spriteImage = uiSpaces[currentIndex].GetComponentInChildren<Image>();

        spriteImage.sprite = item.itemRef.itemSprite;
        texts[0].text = item.attachedSkill.SkillName;
        texts[1].text = item.attachedSkill.SkillRarity;
        texts[2].text = cost.ToString();
    }
    private void SetButton(GameObject skillBook, int cost)
    {
        Button button = uiSpaces[currentIndex].GetComponentInChildren<Button>();
        button.onClick.AddListener(() => BuySkill(skillBook, button, cost));
    }

    public void BuySkill(GameObject skillBook, Button button, int cost)
    {
        int myMoney = manager.CheckCurrent();

        if(myMoney >= cost)
        {
            manager.SubtractValue(cost);
            button.interactable = false;
            inventory.AddToConvoy(skillBook);
        }
    }
}
