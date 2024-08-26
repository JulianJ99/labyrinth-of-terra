using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Terra { 
public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    public List<ScriptableUnit> _units;

    public BaseHero SelectedHero;

    BaseUnit Unit;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnHeroes() {

            foreach(GameObject partyMember in PartyManager.Instance.partyMembers){
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
                
                randomSpawnTile.SetUnit(partyMember);
            }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        foreach(GameObject enemy in EnemyManager.Instance.enemyList){
                Debug.Log("Enemy position reset");
                var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();
                
                randomSpawnTile.SetUnit(enemy);
        }

        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    // private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
    //     return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    // }

    public void SetSelectedHero(BaseHero hero) {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    public void TurnReset(){
        Debug.Log(_units);
        foreach (GameObject partyMember in PartyManager.Instance.partyMembers){
            partyMember.GetComponent<BaseUnit>().TurnReady = true;
            partyMember.GetComponent<SpriteRenderer>().color = Color.white;
           
        }  
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }
}
}
