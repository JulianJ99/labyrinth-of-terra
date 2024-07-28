using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject,_selectedHeroMovementObject,_selectedHeroMovementResetObject,_tileObject,_tileUnitObject;

    void Awake() {
        Instance = this;
    }

    void Update(){
        
    }

    public void ShowTileInfo(Tile tile) {

        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero) {
        if (hero == null) {
            _selectedHeroObject.SetActive(false);
            return;
        }

        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _selectedHeroMovementObject.GetComponentInChildren<Text>().text = "Remaining movement: " + hero.movementRange;
        _selectedHeroObject.SetActive(true);
        _selectedHeroMovementObject.SetActive(true);
        
    }

    public void ChangeMovementInfo(BaseUnit hero){
        _selectedHeroMovementObject.GetComponentInChildren<Text>().text = "Remaining movement: " + hero.movementRange;
    }

    public void ToggleMovementReset(){
        _selectedHeroMovementResetObject.SetActive(true);
    }

    public void MovementReset(){
        _selectedHeroMovementResetObject.SetActive(false);
    }
}
