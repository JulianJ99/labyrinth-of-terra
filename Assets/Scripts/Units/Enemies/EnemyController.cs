using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Terra.ArrowTranslator;
using UnityEditor.Overlays;
using UnityEngine.TextCore.Text;
using Unity.Collections;
using System;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

namespace Terra {


    public class EnemyController : MonoBehaviour {
        public static EnemyController Instance;
        private Scenario bestScenario;
        public RangeFinder rangeFinder;
        public PathFinder pathFinder;
        public List<OverlayTile> path;

        private BaseUnit character;
        [SerializeField] private bool isMoving;
        
        private List<OverlayTile> rangeFinderTiles;
        public List<OverlayTile> weaponRangeFinderTiles;
        private int turnsEnded;

        void Awake(){
            Instance = this;
        }
        void Start(){
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
        }

        void Update(){
            if(path.Count > 0)
                MoveAlongPath();
        }



        public IEnumerator CalculateBestScenario(BaseUnit character){
            
            var tileInMovementRange = rangeFinder.GetTilesInRange(new Vector2Int(character.standingOnTile.gridLocation.x, character.standingOnTile.gridLocation.y), character.movementRange);
            

            var scenario = new Scenario();
            foreach(var tile in tileInMovementRange){
                var tempScenario = CreateTileScenarioValue(tile);

                if(tempScenario != null && tempScenario.scenarioValue > scenario.scenarioValue){
                    scenario = tempScenario;
                    
                }

                if(tempScenario != null && tempScenario.scenarioValue == scenario.scenarioValue){
                    
                    var tempScenarioPathCount = pathFinder.FindPath(character.standingOnTile, tempScenario.positionTile, tileInMovementRange).Count;
                    var scenarioPathCount = pathFinder.FindPath(character.standingOnTile, scenario.positionTile, tileInMovementRange).Count;

                    if(tempScenarioPathCount < scenarioPathCount)
                        scenario = tempScenario;
                    
                }

                if(tempScenario.positionTile == null && !scenario.targetTile){
                    var targetCharacter = FindClosestToDeathCharacter(tile);
                    var scenarioFullPath = pathFinder.FindPath(character.standingOnTile, targetCharacter, new List<OverlayTile>());
                    if(scenarioFullPath.Count > character.GetComponent<CharacterInventory>().equippedWeapon.range + character.movementRange){
                        var scenarioPath = scenarioFullPath.GetRange(0, character.movementRange);
                        var scenarioValue = scenarioPath.Count - targetCharacter.OccupiedUnit.health;

                        if (scenarioValue < scenario.scenarioValue || !scenario.positionTile )
                            scenario = new Scenario(scenarioValue, null, null);
                        
                    }
                }
            }

            bestScenario = scenario;
            if (bestScenario.positionTile) {
                bestScenario.positionTile.ShowTile();
                
                path = pathFinder.FindPath(character.standingOnTile, bestScenario.positionTile, new List<OverlayTile>());

                if(path.Count == 0 && bestScenario.targetTile != null){

                    PositionCharacterOnTile(bestScenario.positionTile);
                    bestScenario.positionTile.OccupiedUnit = character;
                    Attack();
                }
            }
            yield return null;
        }

        private Scenario AttackTarget(OverlayTile position){
            var targetCharacter = FindClosestToDeathCharacter(position);
            if(targetCharacter){
                var closestDistance = pathFinder.GetManhattenDistance(position, targetCharacter);
                var scenarioValue = 0;
                if(closestDistance <= character.movementRange){
                    if(character.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                        scenarioValue = character.strength + character.GetComponent<CharacterInventory>().equippedWeapon.might
                        + closestDistance
                        - targetCharacter.OccupiedUnit.health;
                    }
                    else{
                        scenarioValue = character.magic + character.GetComponent<CharacterInventory>().equippedWeapon.might
                        + closestDistance
                        - targetCharacter.OccupiedUnit.currentHealth;
                    }


                    //Kill takes priority
                    if(character.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                        if(targetCharacter.OccupiedUnit.health < character.strength + character.GetComponent<CharacterInventory>().equippedWeapon.might){
                            scenarioValue = 10000;
                        }
                    }
                    else{
                         if(targetCharacter.OccupiedUnit.health < character.magic + character.GetComponent<CharacterInventory>().equippedWeapon.might){
                            scenarioValue = 10000;
                        }             
                                  
                    }

                    return new Scenario(scenarioValue, targetCharacter, position);
                }
            }

            return new Scenario();
        }

        private OverlayTile FindClosestToDeathCharacter(OverlayTile position, int range = 0){
            OverlayTile targetTile = null;
            int lowestHealth = -1;
            var noCharacterInRange = true;
            foreach(GameObject partyMember in PartyManager.Instance.partyMembers){
                if(partyMember.GetComponent<BaseUnit>().health >= 0 ){
                    var currentDistance = pathFinder.GetManhattenDistance(position, partyMember.GetComponent<BaseUnit>().standingOnTile);
                    var currentHealth = (int)partyMember.GetComponent<BaseUnit>().currentHealth;

                    if (currentDistance <= character.GetComponent<CharacterInventory>().equippedWeapon.range && 
                        ((lowestHealth == -1) || currentHealth <= lowestHealth || noCharacterInRange)){
                            lowestHealth = currentHealth;
                            targetTile = partyMember.GetComponent<BaseUnit>().standingOnTile;
                            noCharacterInRange = false;
                        }
                    else if(noCharacterInRange && ((lowestHealth == -1) || (currentHealth <= lowestHealth))){
                        lowestHealth = currentHealth;
                        targetTile = partyMember.GetComponent<BaseUnit>().standingOnTile;
                    }    
                }
            }
            
            return targetTile;
        }

        private void PositionCharacterOnTile(OverlayTile tile)
        {
            character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
            character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder +1;
            character.standingOnTile = tile;
            tile.OccupiedUnit = character;
            tile.isOccupied = true;
        }

        private void MoveAlongPath(){
            var step = 3 * Time.deltaTime;
            isMoving = true;

            character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);

            if(Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f){
                PositionCharacterOnTile(path[0]);

                if(path.Count == 1){
                    character.transform.GetChild(0).gameObject.SetActive(false);
                    path[0].OccupiedUnit = character;
                    
                    
                    
                }
            }
        }

        private Scenario CreateTileScenarioValue(OverlayTile overlayTile){
            var attackScenario = AttackTarget(overlayTile);
            
            return attackScenario;
        }

        private void Attack(){
            if(bestScenario.targetTile.OccupiedUnit){
                StartCoroutine(AttackTargettedCharacter(bestScenario.targetTile.OccupiedUnit));
            }
            else
            EndTurn();
                
        }

        private IEnumerator AttackTargettedCharacter(BaseUnit targetedCharacter){
            yield return new WaitForSeconds(0.5f);
            
            BaseUnit attacker = character;
            BaseUnit defender = targetedCharacter;
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
            

            int attackerHitRate = attacker.dexterity * 2 + (int)attacker.GetComponent<CharacterInventory>().equippedWeapon.hitChance - defender.speed * 2;
            if(attackerHitRate <= 0){
                attackerHitRate = 0;
            }
            
            int defenderHitRate = defender.dexterity * 2 + (int)defender.GetComponent<CharacterInventory>().equippedWeapon.hitChance - attacker.speed * 2;
            if(defenderHitRate <= 0){
                defenderHitRate = 0;
            }
            

            int attackerCritRate = attacker.luck + (int)attacker.GetComponent<CharacterInventory>().equippedWeapon.critChance - defender.luck;
            if(attackerCritRate <= 0){
                attackerCritRate = 0;
            }
            
            int defenderCritRate = defender.luck + (int)defender.GetComponent<CharacterInventory>().equippedWeapon.critChance - attacker.luck;
            if(defenderCritRate <= 0){
                defenderCritRate = 0;
            }
            CombatManager.Instance.Attack(attacker, defender, attackerHitRate, defenderHitRate, attackerDamage, defenderDamage, attackerCritRate, defenderCritRate);

        }

        public void EndTurn(){
            isMoving = false;
            character.TurnReady = false;
            character.GetComponent<SpriteRenderer>().color = Color.gray;
            
            foreach(OverlayTile tile in rangeFinderTiles){
                if(tile.OccupiedUnit != null){
                    if(tile.OccupiedUnit.GetComponent<BaseUnit>() == character && tile.transform.position != character.transform.position ){
                        tile.OccupiedUnit = null;
                        tile.isOccupied = false;
                    }
                }
            }
            foreach(OverlayTile tile in weaponRangeFinderTiles){
                tile.ResetTile();
            }
            character = null;
            
            turnsEnded += 1;
            if(turnsEnded == EnemyManager.Instance.enemyList.Count()){
                GameManager.Instance.ChangeState(GameState.TurnReset);
                turnsEnded = 0;
            }
            
        }
    }
}