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

        public void SetUnit(BaseUnit unit) {   
        
        if (unit.standingOnTile != null) unit.standingOnTile.OccupiedUnit = null;
        unit.transform.position = gridLocation;
        OccupiedUnit = unit;
        unit.standingOnTile = this;
        
    }

    }
}
