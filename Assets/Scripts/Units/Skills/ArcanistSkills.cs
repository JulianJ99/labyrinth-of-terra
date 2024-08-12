using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanistSkills : MonoBehaviour
{
    public Skills[] rareSkills;
    public Skills[] epicSkills;
    public Skills[] legendarySkills;
    public Skills[] divineSkills;

    public Skills[] allSkills;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AssignArray();
    }

    private void AssignArray()
    {
        int totalSize = rareSkills.Length + epicSkills.Length + legendarySkills.Length + divineSkills.Length;

        allSkills = new Skills[totalSize];

        int currentIndex = 0;

        rareSkills.CopyTo(allSkills, currentIndex);
        currentIndex += rareSkills.Length;

        epicSkills.CopyTo(allSkills, currentIndex);
        currentIndex += epicSkills.Length;

        legendarySkills.CopyTo(allSkills, currentIndex);
        currentIndex += legendarySkills.Length;

        divineSkills.CopyTo(allSkills, currentIndex);
    }
}
