using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PutCube : MonoBehaviour
{
    bool creating;
    ShowMousePosition pointer;
    AreaSelector finalSelector;
    public GameObject CubePrefab;

    public float generationStepDelay;

    [Range(1,10) ]
    public int height = 3;

    public List<AreaSelector> selectors;
    public List<MapBehaviour> Maps;
    // Start is called before the first frame update
    void Start()
    {
        pointer = FindObjectOfType<ShowMousePosition>();

        finalSelector = selectors[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Vector3 startPos = pointer.GetWorldPoint();
            startPos = pointer.snapPosition(startPos, pointer.size);
            if ((AssignZones.IsPointInTile(startPos, AssignZones.PublicTiles) || AssignZones.IsPointInTile(startPos, AssignZones.PrivateTiles)
                || AssignZones.IsPointInTile(startPos, AssignZones.OtherTiles)) && pointer._curGameObject.tag != "tiles")
                Destroy(pointer._curGameObject);
                
        }

        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 startPos = pointer.GetWorldPoint();
            startPos = pointer.snapPosition(startPos, pointer.size);
         
            if (AssignZones.IsPointInTile(startPos, AssignZones.PublicTiles))
            {
                finalSelector = selectors[0];
                //DestroyCells(startPos, AssignZones.PublicTiles);
            }

            if (AssignZones.IsPointInTile(startPos, AssignZones.PrivateTiles))
            {
                finalSelector = selectors[1];
                //DestroyCells(startPos, AssignZones.PrivateTiles);
            }

            if (AssignZones.IsPointInTile(startPos, AssignZones.OtherTiles))
            {
                finalSelector = selectors[2];
                //DestroyCells(startPos, AssignZones.OtherTiles);
            }
                

            var param = new object[2] { startPos, finalSelector };

            PutCollapsedUnit(param);

        }
        //PutCubes();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UndoCollapse(finalSelector.MapBehaviour);
        }

            if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Maps[0].Clear();
            var tiles = AssignZones.PublicTiles;
            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Public", tiles });
            //Generate(new object[2] { "AreaSelector_Public", tiles });
            Debug.Log(tiles.Count);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Maps[1].Clear();
            var tiles = AssignZones.PrivateTiles;
            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Private", tiles });
            //Generate(new object[2] { "AreaSelector_Private", tiles });
            Debug.Log(tiles.Count);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Maps[2].Clear();
            var tiles = AssignZones.OtherTiles;
            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Other", tiles });
            //Generate(new object[2] { "AreaSelector_Other", tiles });
            Debug.Log(tiles.Count);
        }


    }

    private void PutCubes()
    {
        Vector3 startPos = pointer.GetWorldPoint();
        startPos = pointer.snapPosition(startPos, pointer.size);

        GameObject startPole = Instantiate(CubePrefab, startPos, Quaternion.identity);
        startPole.transform.position = new Vector3(startPos.x, startPos.y, startPos.z);

    }

    public void PutCollapsedUnit(object[] param)
    {
        Vector3 site = (Vector3)param[0];
        AreaSelector areaSelector = (AreaSelector)param[1];
        if (!areaSelector.MapBehaviour.Initialized)
        {
            areaSelector.MapBehaviour.Initialize();
        }

        var position = Vector3Int.RoundToInt(site / InfiniteMap.BLOCK_SIZE);

        areaSelector.MapBehaviour.Map.Collapse(position, areaSelector.Size, true);
        areaSelector.MapBehaviour.BuildAllSlots();

    }

    public IEnumerator Generate(object[] param)
    {
        string name = (string)param[0];
        List<Tile> tiles = (List<Tile>)param[1];

        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);

        for (int n = 0; n < height; n++)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                var areaSelector = GameObject.Find(name).GetComponent<AreaSelector>();
                yield return delay;

                var position = new Vector3(tiles[i].transform.position.x, tiles[i].transform.position.y + n * GridManager.size, tiles[i].transform.position.z);
                PutCollapsedUnit(new object[2] { position, areaSelector });
                
            }
        }
       
    }

    public IEnumerator Generate2(object[] param)
    {

        AreaSelector areaSelector = (AreaSelector)param[0];
        List<Vector3> positions = (List<Vector3>)param[1];
        int height = (int)param[2];

        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);

        for (int n = 0; n < height; n++)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                yield return delay;

                var position = new Vector3(positions[i].x, positions[i].y + n * GridManager.size, positions[i].z);
                PutCollapsedUnit(new object[2] { position, areaSelector });

            }
        }

    }


    public void UndoCollapse(MapBehaviour mapBehaviour)
    {
        if (mapBehaviour.Map.History.Any())
        {
            GameObject.DestroyImmediate(mapBehaviour.Map.History.Peek().Slot.GameObject);
            mapBehaviour.Map.Undo(1);
        }
    }
}
