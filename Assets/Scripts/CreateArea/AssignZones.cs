using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignZones : MonoBehaviour
{
    public static List<Tile> tiles;
    AreaCreate area;
    public List<Material> Material;

    public static List<Tile> PublicTiles;
    public static List<Tile> PrivateTiles;
    public static List<Tile> OtherTiles;

    public static Vector3[][] Boundaries;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<Tile>();
        PublicTiles = new List<Tile>();
        PrivateTiles  = new List<Tile>();
        OtherTiles = new List<Tile>();

        Boundaries = new Vector3[3][];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Operate(Zones.Category zone)
    {
        GetArea();
        GetTiles();
        ChangeZone(zone);

    }

    void GetTiles()
    {
        GameObject gameObject = GameObject.Find("Tiles");
        
        tiles.AddRange(gameObject.GetComponentsInChildren<Tile>());
    }

    void GetArea()
    {
         area = GetComponent<AreaCreate>();
        var polygon = area.position.ToArray();

    }

    void ChangeZone(Zones.Category zone)
    {
        Vector3[] positions;
        switch (zone)
        {
            case Zones.Category.Public:
                PublicTiles.AddRange( Change(Material[0], Tile.Status.Public, out positions));
                Boundaries[0] = positions;
                break;

            case Zones.Category.Private: 
                PrivateTiles.AddRange( Change(Material[1], Tile.Status.Private, out positions));
                Boundaries[1] = positions;
                break;

            case Zones.Category.Other:
                OtherTiles.AddRange( Change(Material[2], Tile.Status.Other, out positions));
                Boundaries[2] = positions;
                break;

        }


    }

    public List<Tile> Change(Material material, Tile.Status type, out Vector3[] positions)
    {
        List<Tile> tiles_temp = new List<Tile>();

        var polygon = area.position.ToArray();
        positions = polygon;

        foreach (var tile in tiles)
        {
            var position = tile.GetComponent<Transform>().position;
          
            if (IsPointInPolygon(position, polygon))
            {
                if (tile.status == Tile.Status.Unoccupied)
                {
                    tile.status = type;
                    tiles_temp.Add(tile);
                }
                else if (tile.status == type)
                    continue;
                else
                {
                    if (tile.status == Tile.Status.Public)
                        PublicTiles.Remove(tile);

                    if (tile.status == Tile.Status.Private)
                        PrivateTiles.Remove(tile);

                    if (tile.status == Tile.Status.Other)
                        OtherTiles.Remove(tile);

                    tile.status = type;
                    tiles_temp.Add(tile);

                }
                
                var mat = tile.GetComponent<MeshRenderer>();
                mat.material = material;
            }
        }
  
        return tiles_temp;


    }





    /// <summary>
    /// Judge whether a point is inside or outside a polygon
    /// </summary>
    /// <param name="p"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsPointInPolygon(Vector3 p, Vector3[] polygon)
    {
        float minX = polygon[0].x;
        float maxX = polygon[0].x;
        float minY = polygon[0].z;
        float maxY = polygon[0].z;
        for (int i = 1; i < polygon.Length; i++)
        {
            Vector3 q = polygon[i];
            minX = Mathf.Min(q.x, minX);
            maxX = Mathf.Max(q.x, maxX);
            minY = Mathf.Min(q.z, minY);
            maxY = Mathf.Max(q.z, maxY);
        }

        if (p.x < minX || p.x > maxX || p.z < minY || p.z > maxY)
        {
            return false;
        }

        bool inside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].z > p.z) != (polygon[j].z > p.z) &&
              p.x < (polygon[j].x - polygon[i].x) * (p.z - polygon[i].z) / (polygon[j].z - polygon[i].z) + polygon[i].x)
            {
                inside = !inside;
            }
        }

        return inside;
    }

    public static bool IsPointInTile(Vector3 pos, List<Tile> tiles)
    {
        foreach (var tile in tiles)
        {
            var center = tile.transform.position;
            var v_1 = new Vector3(center.x - GridManager.size * 0.5f, 0, center.z - GridManager.size * 0.5f);
            var v_2 = new Vector3(center.x + GridManager.size * 0.5f, 0, center.z - GridManager.size * 0.5f);
            var v_3 = new Vector3(center.x + GridManager.size * 0.5f, 0, center.z + GridManager.size * 0.5f);
            var v_4 = new Vector3(center.x - GridManager.size * 0.5f, 0, center.z + GridManager.size * 0.5f);

            Vector3[] polygon = new Vector3[4] { v_1, v_2, v_3, v_4 };

            if(IsPointInPolygon(pos, polygon))
                return true;
        }
        return false;
    }
}
