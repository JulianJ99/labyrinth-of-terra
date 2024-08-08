using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Terra {
public class BaseUnit : MonoBehaviour {
    public string UnitName;
    
    public Faction Faction;

    public Animator anim; 

    [SerializeField] public int health;

    [SerializeField] public int attack;

    [SerializeField] public int defense;

    [SerializeField] public int movementRange;

    public OverlayTile standingOnTile;

    public bool TurnReady;

}
}
