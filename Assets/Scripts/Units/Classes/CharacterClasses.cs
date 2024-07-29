using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClasses : MonoBehaviour
{
    public ClassCreation[] classes;

    private ClassCreation currentClass;

    public void RandomizeClass()
    {
        int index = UnityEngine.Random.Range(0, classes.Length);
        currentClass = classes[index];
    }
    public void AdjustStatsGrowths()
    {
        Hero1 statManager = GetComponent<Hero1>();

        statManager.hpvPrio = currentClass.hpvPrio;
        statManager.atkPrio = currentClass.atkPrio;
        statManager.defPrio = currentClass.defPrio;
        statManager.movPrio = currentClass.movPrio;
        statManager.strPrio = currentClass.strPrio;
        statManager.magPrio = currentClass.magPrio;
        statManager.dexPrio = currentClass.dexPrio;
        statManager.spdPrio = currentClass.spdPrio;
        statManager.resPrio = currentClass.resPrio;
        statManager.lckPrio = currentClass.lckPrio;
    }

    public void ChangeCharacter()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Hero1 character = GetComponent<Hero1>();
        spriteRenderer.sprite = currentClass.classSprite;
        character.className = currentClass.className;
    }
}
