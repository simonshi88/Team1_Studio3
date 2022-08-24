using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCollapse : MonoBehaviour
{
    public int length;
    public int width;
    public int height;
    List<Vector3> vectors;
    public AreaSelector areaSelector;
    // Start is called before the first frame update
    void Start()
    {
        vectors = CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            areaSelector.MapBehaviour.Clear();
            GameObject.Find("GridManager").SendMessage("Generate2", new object[3] { areaSelector, vectors, height});
        }

    }
    List<Vector3> CreateGrid()
    {
        var positions = new List<Vector3>();

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                positions.Add(new Vector3(transform.position.x + i * GridManager.size, 0, transform.position.z + j * GridManager.size));
            }
        }

        return positions;
    }
    }
