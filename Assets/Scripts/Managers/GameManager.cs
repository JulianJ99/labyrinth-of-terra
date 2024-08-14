using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terra { 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                
                GridManager.Instance.GenerateGrid();
                
                break;
            case GameState.SpawnHeroes:

                //UnitManager.Instance.SpawnHeroes();
                break;

            case GameState.SpawnEnemies:
                //UnitManager.Instance.SpawnEnemies();
                break;

            case GameState.HeroesTurn:
                Debug.Log("Heroes turn!");
                break;

            case GameState.EnemiesTurn:
                Debug.Log("Enemies turn!");
                Instance.ChangeState(GameState.TurnReset);
                break;

            case GameState.TurnReset:
                UnitManager.Instance.TurnReset();
                break;      
                         
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,
    TurnReset = 5
}
}
