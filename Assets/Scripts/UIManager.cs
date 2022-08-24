using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button generatePublicButton;

    [SerializeField]
    private Button generatePrivateButton;

    [SerializeField]
    private Button generateOtherButton;


    private void Awake()
    {
        Cursor.visible = true;
    }

    void Update()
    {

    }

    void Start()
    {


        generatePublicButton.onClick.AddListener(() =>
        {
            var tiles = AssignZones.PublicTiles;

            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Public", tiles });

            Debug.Log(AssignZones.PublicTiles.Count);
        });

        generatePrivateButton.onClick.AddListener(() =>
        {
            var tiles = AssignZones.PrivateTiles;

            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Private", tiles });

            Debug.Log(AssignZones.PrivateTiles.Count);
        });

        generateOtherButton.onClick.AddListener(() =>
        {
            var tiles = AssignZones.OtherTiles;
    
            GameObject.Find("GridManager").SendMessage("Generate", new object[2] { "AreaSelector_Other", tiles });

            Debug.Log(AssignZones.OtherTiles.Count);
        });


        //other method to generate entire results
        //generatePublicButton.onClick.AddListener(() =>
        //{
        //    var tiles = AssignZones.PublicTiles;
        //    for (int i = 0; i < tiles.Count; i++)
        //    {
        //        var areaSelector = GameObject.Find("AreaSelector_Public").GetComponent<AreaSelector>();
        //        GameObject.Find("GridManager").SendMessage("PutCollapsedUnit", new object[2] { tiles[i].transform.position, areaSelector });

        //    }
        //    Debug.Log(AssignZones.PublicTiles.Count);
        //});
    }
}
