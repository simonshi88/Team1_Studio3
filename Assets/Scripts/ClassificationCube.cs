using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClassificationCube : MonoBehaviour
{
    private MeshRenderer[] AllChildren;

    public bool toggle;
    public List<Material> materials;

    private string[] names;

    public int[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        toggle = false;

        names = new string[materials.Count] ;

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = materials[i].name;
            //Debug.Log(names[i]);
        }

        numbers = new int[names.Length];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Classify();
    }

    private void Classify()
    {
        AllChildren = GetComponentsInChildren<MeshRenderer>();

        var allRooms = FindTags(AllChildren, names);
        Debug.Log(allRooms);
        for (int i = 0; i < allRooms.Length; i++)
        {
            var material = materials[i];
            numbers[i] = allRooms[i].Count;
            for (int j = 0; j < allRooms[i].Count; j++)
            {
                var mat = allRooms[i][j];
                mat.material = material;
            }
        }

        toggle = false;
    }

    List<MeshRenderer>[] FindTags(MeshRenderer[] allTags, string[] name)
    {
        List<MeshRenderer>[] objects = new List<MeshRenderer>[name.Length] ;
        for (int i = 0; i < name.Length; i++)
        {
            objects[i] = allTags.Where(child => child.tag == name[i]).ToList();
        }

        return objects;
    }


}
