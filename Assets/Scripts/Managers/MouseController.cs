using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Terra.ArrowTranslator;
using UnityEditor.Overlays;
using UnityEngine.TextCore.Text;
using Unity.Collections;
using System;
using TMPro;

namespace Terra
{
    public class MouseController : MonoBehaviour
    {
        public GameObject cursor;
        public float speed;
        
        public BaseUnit character;

        private PathFinder pathFinder;
        private RangeFinder rangeFinder;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> path;
        private List<OverlayTile> rangeFinderTiles;
        public List<OverlayTile> weaponRangeFinderTiles;
        [SerializeField] private bool isMoving;

        private int turnsEnded;

        [SerializeField] public GameObject TurnMenu; 

        [SerializeField] public GameObject CombatMenu;

        [SerializeField] public GameObject playerName;

        [SerializeField] public GameObject playerHealth;
        [SerializeField] public GameObject enemyHealth;
        [SerializeField] public GameObject playerHit;
        [SerializeField] public GameObject enemyHit;
        [SerializeField] public GameObject playerDamage;
        [SerializeField] public GameObject enemyDamage;
        [SerializeField] public GameObject playerCrit;
        [SerializeField] public GameObject enemyCrit;
        public static MouseController Instance;        

        private void Awake(){
            Instance = this;
        }

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            arrowTranslator = new ArrowTranslator();

            path = new List<OverlayTile>();
            isMoving = false;
            rangeFinderTiles = new List<OverlayTile>();
            
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();
            
            if (hit.HasValue)
            {
                
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                cursor.transform.position = tile.transform.position;
                
                //GetInWeaponRangeTiles();
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.transform.GetComponent<SpriteRenderer>().sortingOrder +1;

                if (rangeFinderTiles.Contains(tile) && !isMoving && character != null)
                {
                    
                    path = pathFinder.FindPath(character.standingOnTile, tile, rangeFinderTiles);
                   
                    foreach (var item in rangeFinderTiles)
                    {
                        GridManager.Instance.map[item.grid2DLocation].SetSprite(ArrowDirection.None);
                    }

                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : character.standingOnTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                        var arrow = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                        path[i].SetSprite(arrow);
                    }
                }
                else{
                    foreach (var item in rangeFinderTiles)
                    {
                    GridManager.Instance.map[item.grid2DLocation].SetSprite(ArrowDirection.None);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    
                    tile.ShowTile();

                    if (character == null)
                    {
                        character = tile.OccupiedUnit.GetComponent<BaseUnit>();
                        if(character.TurnReady == true){
                            PositionCharacterOnLine(tile);
                            GetInRangeTiles();
                        }
                        else{
                            character = null;
                        }

                    }
                    else if (isMoving && tile.gameObject.GetComponent<OverlayTile>().GetComponent<SpriteRenderer>().sprite.name == "SelectedTileWeapon" && tile.OccupiedUnit.GetComponent<BaseUnit>().Faction != character.Faction){
                        CombatMenuReveal(character, tile.OccupiedUnit);
                                             
                    }
                    else
                    {
                        isMoving = true;
                        tile.gameObject.GetComponent<OverlayTile>().HideTile();
                    }
                }
            }

            if (path.Count > 0 && isMoving)
            {
                MoveAlongPath();
            }
        }

        private void MoveAlongPath()
        {
            
            var step = speed * Time.deltaTime;
            
            float zIndex = path[0].transform.position.z;
            character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
            character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);
            
            if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.00001f)
            {
                
                PositionCharacterOnLine(path[0]);
                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                
                
                TurnMenu.SetActive(true);

            }

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
            if(turnsEnded == PartyManager.Instance.partyMembers.Count()){
                GameManager.Instance.ChangeState(GameState.EnemiesTurn);
                turnsEnded = 0;
            }
            TurnMenu.SetActive(false);
        }

        public void AttackRange(){
            GetInWeaponRangeTiles();
        }

        public void CancelMenu(){
            TurnMenu.SetActive(true);
            CombatMenu.SetActive(false);
        }

        public void CombatMenuReveal(BaseUnit attacker, BaseUnit defender){
            TurnMenu.SetActive(false);
            playerName.GetComponent<TMP_Text>().text = attacker.name;

            playerHealth.GetComponent<TMP_Text>().text = attacker.health.ToString();
            enemyHealth.GetComponent<TMP_Text>().text = defender.health.ToString();

            
            
            int attackerDamage = 0;
            int defenderDamage = 0;
            if(attacker.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                
                attackerDamage = attacker.strength + attacker.GetComponent<CharacterInventory>().equippedWeapon.might - defender.defense;
            }
            else{
                
                attackerDamage = attacker.magic + attacker.GetComponent<CharacterInventory>().equippedWeapon.might - defender.resistance;
            }
            playerDamage.GetComponent<TMP_Text>().text = attackerDamage.ToString();

            if(defender.GetComponent<CharacterInventory>().equippedWeapon.damageType == "Physical"){
                defenderDamage = defender.strength + defender.GetComponent<CharacterInventory>().equippedWeapon.might - attacker.defense;
            }
            else{
                defenderDamage = defender.magic + defender.GetComponent<CharacterInventory>().equippedWeapon.might - attacker.resistance;
            }
            enemyDamage.GetComponent<TMP_Text>().text = defenderDamage.ToString();

            int attackerHitRate = attacker.dexterity * 2 + (int)attacker.GetComponent<CharacterInventory>().equippedWeapon.hitChance - defender.speed * 2;
            if(attackerHitRate <= 0){
                attackerHitRate = 0;
            }
            playerHit.GetComponent<TMP_Text>().text = attackerHitRate.ToString();
            int defenderHitRate = defender.dexterity * 2 + (int)defender.GetComponent<CharacterInventory>().equippedWeapon.hitChance - attacker.speed * 2;
            if(defenderHitRate <= 0){
                defenderHitRate = 0;
            }
            enemyHit.GetComponent<TMP_Text>().text = defenderHitRate.ToString();

            int attackerCritRate = attacker.luck + (int)attacker.GetComponent<CharacterInventory>().equippedWeapon.critChance - defender.luck;
            if(attackerCritRate <= 0){
                attackerCritRate = 0;
            }
            playerCrit.GetComponent<TMP_Text>().text = attackerCritRate.ToString();
            int defenderCritRate = defender.luck + (int)defender.GetComponent<CharacterInventory>().equippedWeapon.critChance - attacker.luck;
            if(defenderCritRate <= 0){
                defenderCritRate = 0;
            }
            enemyCrit.GetComponent<TMP_Text>().text = defenderCritRate.ToString();


            CombatMenu.SetActive(true);
            
            UnityEngine.UI.Button AttackButton = GameObject.Find("AttackButton").GetComponent<UnityEngine.UI.Button>();
            AttackButton.onClick.RemoveAllListeners();
            AttackButton.onClick.AddListener(() => CombatManager.Instance.Attack(attacker, defender, attackerHitRate, defenderHitRate, attackerDamage, defenderDamage, attackerCritRate, defenderCritRate)); 
        }
        

        private void PositionCharacterOnLine(OverlayTile tile)
        {
            character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
            character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder +1;
            character.standingOnTile = tile;
            tile.OccupiedUnit = character;
            tile.isOccupied = true;
        }

        private static RaycastHit2D? GetFocusedOnTile()
        {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }

        private void GetInRangeTiles()
        {
            rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int(character.standingOnTile.gridLocation.x, character.standingOnTile.gridLocation.y), character.movementRange);

            foreach (var item in rangeFinderTiles)
            {
                item.ShowTile();
            }
        }

        private void GetInWeaponRangeTiles()
        {
            
            if(character != null && character.GetComponent<CharacterInventory>() != null){
                
                weaponRangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int(character.standingOnTile.gridLocation.x, character.standingOnTile.gridLocation.y), character.GetComponent<CharacterInventory>().equippedWeapon.weaponRef.weaponRange);

                foreach (var item in weaponRangeFinderTiles)
                {
                    item.ShowWeaponTile();
                    if(character.GetComponent<CharacterInventory>().equippedWeapon.weaponRef.weaponType == "Ranged Jab"){
                        RemoveAdjacentTiles();
                    }
                        
                }
                
            }

        }

        private void RemoveAdjacentTiles(){
            foreach (var item in weaponRangeFinderTiles)
            {
                if(item.grid2DLocation.x == character.standingOnTile.gridLocation.x + 1 && item.grid2DLocation.y == character.standingOnTile.gridLocation.y || item.grid2DLocation.x == character.standingOnTile.gridLocation.x - 1 && item.grid2DLocation.y == character.standingOnTile.gridLocation.y || 
                   item.grid2DLocation.y == character.standingOnTile.gridLocation.y + 1 && item.grid2DLocation.x == character.standingOnTile.gridLocation.x || item.grid2DLocation.y == character.standingOnTile.gridLocation.y - 1 && item.grid2DLocation.x == character.standingOnTile.gridLocation.x){
                    item.HideTile();
                }
                else
                {
                    ;
                }
            }
        }

        
    }

    public static class VectorExtensions
    {
        public static Vector2Int ClosestGrid(this Vector3 vec)
        {
            var xr = Math.Round(vec.x / 2f, MidpointRounding.AwayFromZero) * 2f;
            var yr = Math.Round(vec.y / 2f, MidpointRounding.AwayFromZero) * 2f;
            return new Vector2Int((int)xr, (int)yr);
        }
    }
}
