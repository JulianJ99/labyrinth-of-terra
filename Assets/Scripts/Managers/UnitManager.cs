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
        var heroCount = 1;

        for (int i = 0; i < heroCount; i++) {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            Debug.Log(randomPrefab);
            var spawnedHero = Instantiate(randomPrefab);
            Debug.Log(spawnedHero);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            Debug.Log(randomSpawnTile);
            randomSpawnTile.SetUnit(spawnedHero);
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

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero) {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    public void TurnReset(){
        Debug.Log(_units);
        foreach (BaseUnit unit in instantiatedUnits){
            unit.TurnReady = true;
            unit.GetComponent<SpriteRenderer>().color = Color.white;
           
        }  
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }
}
}
