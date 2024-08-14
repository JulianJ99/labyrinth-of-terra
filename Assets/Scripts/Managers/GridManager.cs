using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Terra { 
public class GridManager : MonoBehaviour {
    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }
    [SerializeField] private int _width, _height;

    [SerializeField] private TileBase _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    public Dictionary<Vector2Int, OverlayTile> map;

    public Dictionary<Vector2Int, Tile> undermap;

    public Dictionary<Vector2Int, Tile> spawnmap;
    

    [SerializeField] public Tilemap blankmap;

    public GameObject overlayPrefab;
    public GameObject overlayContainer;

      



    void Awake() {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
    }

        void Start()
        {
            var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            map = new Dictionary<Vector2Int, OverlayTile>();

            foreach (var tm in tileMaps)
            {
                
                BoundsInt bounds = tm.cellBounds;
                
                for (int z = bounds.max.z; z >= bounds.min.z; z--)
                {
                   
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                       
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            
                            if (tm.HasTile(new Vector3Int(x, y, z)))
                            {
                                if (tm.GetTile(new Vector3Int(x, y, z)) == _mountainTile){

                                }
                                else{
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder + 1;
                                    overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, z);
                                    overlayTile.name = $"Tile {x} {y}";
                                    foreach(BaseUnit unit in UnitManager.Instance.instantiatedUnits){
                                        if(overlayTile.transform.position == unit.transform.position){
                                            Debug.Log("Overlap!");
                                            overlayTile.gameObject.GetComponent<OverlayTile>().OccupiedUnit = unit;
                                            overlayTile.gameObject.GetComponent<OverlayTile>().isOccupied = true;
                                        }
                                    }
                                    //Spawn tiles
                                    map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());
                                    
                                }
                                }

                            }
                        }
                    }
                }
            }
        }


    public void GenerateGrid()
    {
        var tileMaps = gameObject.transform.GetComponentsInChildren<TilemapRenderer>();
        undermap = new Dictionary<Vector2Int, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                foreach(BaseUnit unit in UnitManager.Instance.instantiatedUnits){
                    
                    Vector3 check = new Vector3(x + 0.5f, y + 0.5f, 1);
                    
                    if(check == unit.transform.position){
                        randomTile = _grassTile;
                        Debug.Log("Your ass is grass" + unit.transform.position);
                    }
                }
                TileBase spawnedTile = randomTile;
                
                Vector3Int p = new Vector3Int(x, y, 0);
                blankmap.SetTile(p, spawnedTile);
                //spawnmap.Add(new Vector2Int(x, y), randomTile.gameObject.GetComponent<TileBase>());
                
            }
        }

        
        //Debug.Log("Grid spawned");
        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public OverlayTile GetHeroSpawnTile() {    
                
        return map.Where(t => t.Key.x > _width / 2).OrderBy(t => Random.value).First().Value;
    }

    public OverlayTile GetEnemySpawnTile(){
        return map.Where(t => t.Key.x < _width / 2).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if (undermap.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
    
    public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile)
    {
        var surroundingTiles = new List<OverlayTile>();


        Vector2Int TileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }
        
        return surroundingTiles;
    }
}
}