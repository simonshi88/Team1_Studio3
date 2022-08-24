using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGizmos : MonoBehaviour
{
    public GameObject PreviewPrefab;
    ShowMousePosition pointer;
    GameObject display;
    // Start is called before the first frame update
    void Start()
    {
        pointer = FindObjectOfType<ShowMousePosition>();

        display = Instantiate(PreviewPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = pointer.GetWorldPoint();
        startPos = pointer.snapPosition(startPos, GridManager.size);

        display.transform.position = new Vector3(startPos.x, startPos.y + 0.5f * pointer.size, startPos.z);
        
        display.transform.localScale = new Vector3(GridManager.size, GridManager.size, GridManager.size);
    }
}
