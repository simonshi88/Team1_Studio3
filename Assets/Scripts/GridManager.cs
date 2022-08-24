using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Range(1,100)]
    public int _width, _height;

    [Range(0.1f, 10)]
    public static float size = 3F;

    public Tile _tilePrefab;

    public Transform _cam;

    public static Dictionary<Vector2, Tile> _tiles;

    public GameObject parent;

    private void Awake()
    {
    }

    void Start()
    {
        GenerateGrid();
        DontDestroyOnLoad(parent);
    }

    void GenerateGrid()
    {
        parent = new GameObject();
        parent.name = "Tiles";
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(transform.position.x + x * size, 0, transform.position.z + z * size), Quaternion.identity);
                
                spawnedTile.transform.localScale = new Vector3(size, 0.1f, size);
                spawnedTile.name = $"Tile {x} {z}";

                var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
                //spawnedTile.Init(isOffset);
                spawnedTile.transform.parent = parent.transform;

                _tiles[new Vector2(x, z)] = spawnedTile;
            }
        }

        //_cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }


}
