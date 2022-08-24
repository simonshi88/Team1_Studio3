using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMousePosition : MonoBehaviour
{
    //public GameObject mousePointer;
    public Camera mainCamera;

    public GameObject _curGameObject;
    [Range(0.1f, 5)]
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = GetWorldPoint();
        transform.position = snapPosition(GetWorldPoint(), size);
    }

    public Vector3 GetWorldPoint()
    {
        //Camera cam = GetComponent<Camera>();

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _curGameObject = hit.transform.gameObject;
            return hit.point;
        }
        

        return Vector3.zero;
    }

    public Vector3 snapPosition (Vector3 original, float size)
    {
        Vector3 snapped;
        snapped.x = Mathf.Round(original.x / size ) * size;
        snapped.y = Mathf.Round(original.y / size ) * size;
        snapped.z = Mathf.Round(original.z / size ) * size;

        return snapped;
    }


}
