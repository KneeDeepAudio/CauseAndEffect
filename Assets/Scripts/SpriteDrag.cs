﻿using UnityEngine;

public class SpriteDrag : MonoBehaviour
{

    private PlacementArea[] placementAreas;
    private Vector3 startLocalPos;
    private ObjectSpawn spawnArea;
    private bool mouseDown = false;
    private Vector3 startMousePos;
    private Vector3 startPos;
    private float offsetX, offsetY;
    private Collider2D[] colliders;

    void Awake()
    {
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
        colliders = GetComponents<Collider2D>();
    }

    void Start ()
    {

    }

    void Update()
    {
        if (mouseDown)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(target.x + offsetX, target.y + offsetY, 0f);
        }
    }


    public void OnMouseDown()
    {
        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offsetX = transform.position.x - offset.x;
        offsetY = transform.position.y - offset.y;

        startLocalPos = transform.localPosition;
        spawnArea = gameObject.GetComponent<PlaceableObject>().spawnArea;
        SetLayerRecursively(LayerMask.NameToLayer("IgnoreCollision"));

        mouseDown = true;
        spawnArea.full = false;

        if (gameObject.tag == "Object")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightObjectPlacement();
            }
        }
        else if (gameObject.tag == "Block")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightBlockPlacement();
            }
        }
    }

    public void OnMouseUp()
    {
        SetLayerRecursively(LayerMask.NameToLayer("Object"));
        mouseDown = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        transform.localPosition = startLocalPos;

        // If player hits an object placement area pass the object to place to the area
        if (hit && hit.collider.tag == "ObjectPlacement")
        {
            Debug.Log("Area Hit");
            ObjectSpawn areaPlaced = hit.collider.gameObject.GetComponent<ObjectSpawn>();
            if (areaPlaced == spawnArea)
            {
                spawnArea.full = true;
            }
            else
            {
                areaPlaced.PlaceObject(gameObject);
                spawnArea.RemoveObject();
            }
        }
        else
        {
            PlaceableObject placedObject = gameObject.GetComponent<PlaceableObject>();
            ObjectSpawn areaPlaced = placedObject.spawnArea;
            areaPlaced.RemoveObject();
        }

        foreach (PlacementArea area in placementAreas)
        {
            area.RemoveHighlight();
        }
    }

    public void SetLayerRecursively(int layerNumber)
    {
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}