using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace finished3
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }

        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        public Dictionary<Vector2Int, OverlayTile> map;

        private void Awake()
        {
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
Debug.Log(bounds);
                for (int z = bounds.max.z; z > bounds.min.z; z--)
                {
                    Debug.Log("Test Z");
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        Debug.Log("Test Y");
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            Debug.Log("Test X");
                            if (tm.HasTile(new Vector3Int(x, y, z)))
                            {
                                Debug.Log("Tile");
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    Debug.Log("No key");
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
