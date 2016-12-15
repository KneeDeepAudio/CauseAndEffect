using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour {

    public GameObject prefab;
    public UIManager uiManager;

    private float offsetX, offsetY;
    private GameManager manager;
    private PlacementArea[] placementAreas;
    private Vector3 startPos;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
    }

    void Start()
    {
        startPos = transform.localPosition;
    }

    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;

        if(prefab.tag == "Object")  
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightObjectPlacement();
            }
        }
        else if (prefab.tag == "Block")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightBlockPlacement();
            }
        }

    }

    public void OnDrag()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y   );
    }

    public void EndDrag()
    {
        transform.localPosition = startPos;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // If player hits an object placement area pass the object to place to the area
        if (hit && hit.collider.tag == "ObjectPlacement")
        {
            Debug.Log("Area Hit");
            ObjectSpawn areaPlaced = hit.collider.gameObject.GetComponent<ObjectSpawn>();
            areaPlaced.PlaceObject(prefab);
        }

        foreach (PlacementArea area in placementAreas)
        {
            area.RemoveHighlight();
        }

    }
}
