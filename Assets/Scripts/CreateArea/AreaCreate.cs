using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AreaCreate : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public List<Vector3> position;
    private Vector3 currentPosition;
    private Vector3 mousePosition;

    public Zones.Category zone;

    public Text Text;
    public float Size;
    public GameObject Mouse;
    public float MouseSize;

    private float distance;

    private bool continueDraw;

    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
 
        position = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.Escape))
        {
            StopDrawing();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        { 
            if(lineRenderer.positionCount >= 3)
            {
                var originalPoint = new Vector3(position[0].x, 1, position[0].z);
                position.Add(originalPoint);
                lineRenderer.SetPosition(position.Count - 1, originalPoint);

                SendMessage("Operate", zone);              
            }
            StopDrawing();
            continueDraw = false;

            
        }

        Mouse.transform.position = snapPosition(GetWorldPoint(), Size);

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                continueDraw = true;

                currentPosition = snapPosition(GetWorldPoint(), Size);

                if (position.Count > 1 && currentPosition == position[0])
                {
                    StopDrawing();
                    return;
                }

                position.Add(currentPosition);
                lineRenderer.positionCount = position.Count + 1;
                lineRenderer.SetPosition(position.Count - 1, new Vector3(currentPosition.x, 1, currentPosition.z));
            }           
        }

        if (continueDraw)
        {
            if (position.Count >= 1)
            {
                mousePosition = snapPosition(GetWorldPoint(), Size);
           
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(mousePosition.x, 1, mousePosition.z));

                distance = (mousePosition - position[position.Count - 1]).magnitude;
                Text.text = distance.ToString("F1") + "Meters";
            }
        }


    }


    public Vector3 GetWorldPoint()
    {
        //Camera cam = GetComponent<Camera>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public Vector3 snapPosition(Vector3 original, float size)
    {
        Vector3 snapped;
        snapped.x = Mathf.Round(original.x / size) * size;
        snapped.y = Mathf.Round(original.y / size) * size;
        snapped.z = Mathf.Round(original.z / size) * size;

        return snapped;
    }

    public void StopDrawing()
    {
        continueDraw = false;
        position.Clear();

    }
}
