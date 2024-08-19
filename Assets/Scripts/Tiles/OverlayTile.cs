using System.Collections.Generic;
using UnityEngine;
using static Terra.ArrowTranslator;

namespace Terra
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F { get { return G + H; } }

        public bool isBlocked = false;
        public bool isOccupied = false;

        public OverlayTile Previous;
        public Vector3Int gridLocation;
        public Vector2Int grid2DLocation {get { return new Vector2Int(gridLocation.x, gridLocation.y); } }

        public List<Sprite> arrows;

        public BaseUnit OccupiedUnit;

        [SerializeField] private bool _isWalkable;

        public bool Walkable => _isWalkable && OccupiedUnit == null;

        

        public virtual void Init(int x, int y)
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HideTile();
            }

        }

        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }

        public void ShowTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public void SetSprite(ArrowDirection d)
        {
            if (d == ArrowDirection.None)
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 0);
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = arrows[(int)d];
                GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }

        public void SetUnit(GameObject unit) {   
        
        if (unit.GetComponent<BaseUnit>().standingOnTile != null) unit.GetComponent<BaseUnit>().standingOnTile.OccupiedUnit = null;
        unit.transform.position = gridLocation;
        var properX = unit.transform.position.x + 0.5f;
        var properY = unit.transform.position.y + 0.5f;
        unit.transform.position = new Vector3(properX, properY, unit.transform.position.z);
        OccupiedUnit = unit.GetComponent<BaseUnit>();
        unit.GetComponent<BaseUnit>().standingOnTile = this;
        
    }

    }
}
