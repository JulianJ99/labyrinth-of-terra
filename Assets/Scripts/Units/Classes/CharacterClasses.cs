using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terra {
public class CharacterClasses : MonoBehaviour
{
    public ClassCreation[] classes;
    private ClassCreation currentClass;
    private float randomChance = 0.15f;
    

    public List<Weapons> allWeapons;

    public void RandomizeClass()
    {
        int index = UnityEngine.Random.Range(0, classes.Length);
        currentClass = classes[index];
    }
    public void AdjustStatsGrowths()
    {
        if(GetComponent<Hero1>() != null){
            
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
        else{
            
            Enemy1 statManager = GetComponent<Enemy1>();

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

    }

    public void ChangeCharacter()
    {
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(GetComponent<Hero1>() != null){
           
            Hero1 character = GetComponent<Hero1>();
            spriteRenderer.sprite = currentClass.classSprite;
            character.className = currentClass.className;
        }
        else{
            
            Enemy1 character = GetComponent<Enemy1>();
            spriteRenderer.sprite = currentClass.classSprite;
            character.className = currentClass.className;            
        }

    }

    public void AssignAffinities()
    {
        if(GetComponent<Hero1>() != null){
            
            Hero1 unit = GetComponent<Hero1>();
            unit.weaponsAffinities = new List<WeaponAffinity>();

            foreach (var affinity in currentClass.weaponAffinities)
            {
                unit.weaponsAffinities.Add(new WeaponAffinity { weapon = affinity.weapon, affinity = affinity.affinity });
            }

            if (Random.value < randomChance)
            {
                Weapons randomWeapon = allWeapons[UnityEngine.Random.Range(0, allWeapons.Count)];

                if (!unit.weaponsAffinities.Exists(wa => wa.weapon == randomWeapon))
                {
                    float affinityRange = Random.Range(0f, 100f);
                    unit.weaponsAffinities.Add(new WeaponAffinity { weapon = randomWeapon, affinity = affinityRange });
                }
            }
        }
        else{
            ;
            Enemy1 unit = GetComponent<Enemy1>();
            unit.weaponsAffinities = new List<WeaponAffinity>();

            foreach (var affinity in currentClass.weaponAffinities)
            {
                unit.weaponsAffinities.Add(new WeaponAffinity { weapon = affinity.weapon, affinity = affinity.affinity });
            }

            if (Random.value < randomChance)
            {
                Weapons randomWeapon = allWeapons[UnityEngine.Random.Range(0, allWeapons.Count)];

                if (!unit.weaponsAffinities.Exists(wa => wa.weapon == randomWeapon))
                {
                    float affinityRange = Random.Range(0f, 100f);
                    unit.weaponsAffinities.Add(new WeaponAffinity { weapon = randomWeapon, affinity = affinityRange });
                }
            }
        
        }            
        
    }

    public void GiveWeapon()
    {
        CharacterInventory inven = GetComponent<CharacterInventory>();
        GameObject weaponToAdd = Instantiate(currentClass.classWeapon);

        inven.AddToInventory(weaponToAdd);
        weaponToAdd.transform.parent = transform;
        //Equip first weapon
        if(GetComponent<Hero1>() != null){
            
            Hero1 unit = GetComponent<Hero1>();
            unit.GetComponent<CharacterInventory>().equippedWeapon = weaponToAdd.GetComponent<WeaponManager>();
        } 
        else{
            
            Enemy1 unit = GetComponent<Enemy1>();
            unit.GetComponent<CharacterInventory>().equippedWeapon = weaponToAdd.GetComponent<WeaponManager>();
        }
    }

    public void SetSkills()
    {
        SkillManager skills = GetComponent<SkillManager>();
        skills.AddSkill(currentClass.classSkills[0]);
    }
}
}
