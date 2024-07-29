using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour {
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;

    public Transform movePoint;

    public Animator anim; 

    [SerializeField] public int health;

    [SerializeField] public int attack;

    [SerializeField] public int defense;

    [SerializeField] public int movementRange;

    [SerializeField] public float moveSpeed = 5f;

    [Header("Additional Stats")]
    public int strength;
    public int magic;
    public int dexterity;
    public int speed;
    public int resistance;
    public int luck;

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

    public bool isMoving = false;
    public bool firstPerMove = false;

    public Transform originalMovePoint;

    public int originalMovement;

    void Start(){
        originalMovePoint.position = movePoint.position;
        
    }

    public void CharacterMovement(){


        //Quick and dirty fix for moving outside of the grid
        Vector3 vectorMinimal = new Vector3(-0.1f, -0.1f);
        Vector3 vectorMaximal = new Vector3(15.1f, 8.1f);

        if(isMoving == false && movementRange >= 0){

            if(movePoint.position.x > vectorMinimal.x && movePoint.position.x < vectorMaximal.x && movePoint.position.y > vectorMinimal.y && movePoint.position.y < vectorMaximal.y){
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
                
            }
            else{
                movePoint.position = transform.position;
                return;
                
                
            }
       

            if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
            {
                
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                {


                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f), .2f, whatStopsMovement))
                    {
                            movePoint.position += new Vector3(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), 0f);

                            isMoving = true;
                            StartCoroutine(moveAnim());
                    }
                } 
                
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical")), .2f, whatStopsMovement))
                    {
                            movePoint.position += new Vector3(0f, Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));
                            
                            isMoving = true;
                            StartCoroutine(moveAnim());
                    }
                }


                
            } 
        }

        if (Input.inputString == "\b") {
            print ("Backspace!");
            transform.position = originalMovePoint.position;
            movePoint.position = originalMovePoint.position;
            movementRange = originalMovement;
            MenuManager.Instance.ChangeMovementInfo(this);
            MenuManager.Instance.MovementReset();
        }
    }

    IEnumerator moveAnim(){
        
        yield return new WaitForSeconds(0.25f);
        isMoving = false;
        movUsed();
    }

    public void movUsed(){
        
            Debug.Log("Reducing movement");
            movementRange--;
            if(movementRange >= 0){
                MenuManager.Instance.ChangeMovementInfo(this);
                return;
            }
            else{
                MenuManager.Instance.ToggleMovementReset();
                return;
            }
            
        
    }
}
