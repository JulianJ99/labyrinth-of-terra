using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Terra { 
public class GridManager : MonoBehaviour {
    public static GridManager _instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private TileBase _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    public Dictionary<Vector2Int, OverlayTile> map;

    public Dictionary<Vector2Int, Tile> undermap;

    [SerializeField] public Tilemap blankmap;

        public static GridManager Instance { get { return _instance; } }

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
            undermap = new Dictionary<Vector2Int, Tile>();
            map = new Dictionary<Vector2Int, OverlayTile>();

            foreach (var tm in tileMaps)
            {
                BoundsInt bounds = tm.cellBounds;

                for (int z = bounds.max.z; z > bounds.min.z; z--)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            if (tm.HasTile(new Vector3Int(x, y, z)))
                            {
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder;
                                    overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, z);
    
                                    map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());
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
                TileBase spawnedTile = randomTile;
                
                Vector3Int p = new Vector3Int(x, y, 0);
                blankmap.SetTile(p, spawnedTile);
                
                //Tile spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);


            }
        }

        
        Debug.Log("Grid spawned");
        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawnTile() {
        return undermap.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return undermap.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
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