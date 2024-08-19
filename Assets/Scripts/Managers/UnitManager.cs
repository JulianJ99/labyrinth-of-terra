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
    [SerializeField] public List<BaseUnit> instantiatedUnits;
    public BaseHero SelectedHero;

    BaseUnit Unit;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnHeroes() {

            foreach(GameObject partyMember in PartyManager.Instance.partyMembers){
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
                Debug.Log(randomSpawnTile);
                randomSpawnTile.SetUnit(partyMember);
            }
            
            

        

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        // var enemyCount = 1;

        // for (int i = 0; i < enemyCount; i++)
        // {
        //     var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
        //     var spawnedEnemy = Instantiate(randomPrefab);
        //     var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

        //     randomSpawnTile.SetUnit(spawnedEnemy);
        // }

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
