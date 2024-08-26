
using UnityEngine;
using System;
using Unity.Mathematics;

namespace Terra
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;        

        private void Awake(){
            Instance = this;
        }

        public void Attack(BaseUnit attacker, BaseUnit defender){

            Debug.Log("Attacked");

            int attackerHitRate = attacker.dexterity * 2 + (int)attacker.GetComponent<CharacterInventory>().equippedWeapon.hitChance - defender.speed * 2;
            int defenderHitRate = defender.dexterity * 2 + (int)defender.GetComponent<CharacterInventory>().equippedWeapon.hitChance - attacker.speed * 2;
            int attackerDamage = 0;
            int defenderDamage = 0;
            if(attacker.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                
                attackerDamage = attacker.strength + attacker.GetComponent<CharacterInventory>().equippedWeapon.might - defender.defense;
            }
            else{
                
                attackerDamage = attacker.magic + attacker.GetComponent<CharacterInventory>().equippedWeapon.might - defender.resistance;
            }
            if(defender.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                defenderDamage = defender.strength + defender.GetComponent<CharacterInventory>().equippedWeapon.might - attacker.defense;
            }
            else{
                defenderDamage = defender.magic + defender.GetComponent<CharacterInventory>().equippedWeapon.might - attacker.resistance;
            }
            

            void AttackerCombat(){
                float randValue1 = UnityEngine.Random.Range(1, 100);
                
                if(attackerHitRate > randValue1) {
                    defender.health -= attackerDamage;
                    Debug.Log(attacker + " reduced " + defender + "'s health by " + attackerDamage);
                }
                else {
                    Debug.Log(attacker + " missed!");
                }
            }

            void DefenderCombat(){
                float randValue2 = UnityEngine.Random.Range(1, 100);
                
                if(defender.GetComponent<CharacterInventory>().equippedWeapon.range < attacker.GetComponent<CharacterInventory>().equippedWeapon.range){
                    Debug.Log("Outranged! No counterattack.");
                }
                else{
                    if(defenderHitRate > randValue2){
                        attacker.health -= defenderDamage;
                        Debug.Log(defender + " reduced " + attacker + "'s health by " + defenderDamage);
                    }
                    else{
                        Debug.Log(defender + " missed!");
                    }
                }

            }

            Debug.Log("Round 1!");
            AttackerCombat();

            Debug.Log("Round 2!");
            DefenderCombat();
      
            if(attacker.speed > defender.speed + 4){
                Debug.Log(attacker + " is 5 speed faster! Round 3!");
                AttackerCombat();
            }
            else if(defender.speed > attacker.speed + 4){
                Debug.Log(defender + " is 5 speed faster! Round 3!");
                DefenderCombat();
            }
            else{
                Debug.Log("Speed is evenly matched, no third round.");
            }
        }
    }
}