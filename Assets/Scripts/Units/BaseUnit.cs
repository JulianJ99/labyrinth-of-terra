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
    public int currentHealth;

    [SerializeField] public int attack;

    [SerializeField] public int defense;

    [SerializeField] public int movementRange;

    public OverlayTile standingOnTile;

    public bool TurnReady;


    public string className;

    [Header("Additional Stats")]
    public int strength;
    public int magic;
    public int dexterity;
    public int speed;
    public int resistance;
    public int luck;

    public int cost;

    public List<WeaponAffinity> weaponsAffinities;

    //Variable for equipped weapon so units start with their first weapon equipped

    [Header("EXP System")]
    public int currentLevel;
    public float expAmount;

    [Header("Stat growth Bias")]
    [Range(0f, 1f)] public float hpvPrio;
    [Range(0f, 1f)] public float atkPrio;
    [Range(0f, 1f)] public float defPrio;
    [Range(0f, 1f)] public float movPrio;
    [Range(0f, 1f)] public float strPrio;
    [Range(0f, 1f)] public float magPrio;
    [Range(0f, 1f)] public float dexPrio;
    [Range(0f, 1f)] public float spdPrio;
    [Range(0f, 1f)] public float resPrio;
    [Range(0f, 1f)] public float lckPrio;

    public LayerMask whatStopsMovement;

    public enum LastAxis { None, X, Y }
    public LastAxis lastAxis = LastAxis.None;

    void Start(){
        
        currentHealth = health;
    }

            
        
    
    public void HealUnit(int amount)
    {
        currentHealth += amount;
        if(currentHealth > health)
        {
            currentHealth = health;
        }
    }
}
}
