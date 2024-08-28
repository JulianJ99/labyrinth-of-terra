
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace Terra
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;        

        private void Awake(){
            Instance = this;
        }

        private bool CombatEnd = false;

        public void CharacterDeath(BaseUnit character){
            if(character.Faction == Faction.Hero){
                
            }
            else{

            }
            Debug.Log(character + " died!");
            Destroy(character.gameObject);
            CombatEnd = true; 
        }

        public void Attack(BaseUnit attacker, BaseUnit defender, int attackerHitRate, int defenderHitRate, int attackerDamage, int defenderDamage, int attackerCritRate, int defenderCritRate){

            Debug.Log("Attacked");

            void AttackerCombat(){
                float randValue1 = UnityEngine.Random.Range(1, 100);
                float randValue2 = UnityEngine.Random.Range(1, 100);
                
                if(attackerHitRate > randValue1) {
                    if(attackerCritRate > randValue2){
                        attackerDamage = (int)(attackerDamage * 2.5);
                        Debug.Log(attacker + "does beeeeg damage");
                    }
                    defender.health -= attackerDamage;
                    if(defender.health <= 0){
                        CharacterDeath(defender);
                    }
                    Debug.Log(attacker + " reduced " + defender + "'s health by " + attackerDamage);
                }
                else {
                    Debug.Log(attacker + " missed!");
                }
            }

            void DefenderCombat(){
                float randValue3 = UnityEngine.Random.Range(1, 100);
                float randValue4 = UnityEngine.Random.Range(1, 100);
                if(defender.GetComponent<CharacterInventory>().equippedWeapon.range < attacker.GetComponent<CharacterInventory>().equippedWeapon.range){
                    Debug.Log("Outranged! No counterattack.");
                }
                else{
                    if(defenderHitRate > randValue3){
                        if(defenderCritRate > randValue4){
                            defenderDamage = (int)(defenderDamage * 2.5);
                            Debug.Log(defender + "does beeeeg damage");
                        }
                        attacker.health -= defenderDamage;
                        if(attacker.health <= 0){
                            CharacterDeath(attacker);
                        }
                        Debug.Log(defender + " reduced " + attacker + "'s health by " + defenderDamage);
                    }
                    else{
                        Debug.Log(defender + " missed!");
                    }
                }

            }

            Debug.Log("Round 1!");
            AttackerCombat();
            Debug.Log(CombatEnd);

            if(CombatEnd == false){
                Debug.Log("Round 2!");
                DefenderCombat();
            }
            
            if(CombatEnd == false){
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
            CombatEnd = false;
            MouseController.Instance.EndTurn();
            MouseController.Instance.CombatMenu.SetActive(false);
        }
    }
}