using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject currentObject;
    private GameManager manager;
    private PlacementArea[] placementAreas;

    void Awake()
    {
        manager = GetComponent<GameManager>();
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlaceObject();
        }
        if (Input.GetButtonDown("Fire2"))
            FlipObject();
    }

    public void PlaceObject()
    {
        // First Shoot a ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // If nothing is hit, stop doing things
        if (!hit)
            return;

        //If there is any other object in the area, place nothing
        Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        foreach (Collider2D i in hits)
        {
            if (i.gameObject.tag == "Block" || i.gameObject.tag == "Object" || i.gameObject.tag == "StartBlock")
                return;
        }

                // Check if player is placing object in the right area
        if (hit.collider.tag == "ObjectPlacement")
        {
            //Grab area
            ObjectSpawn areaPlaced = hit.collider.gameObject.GetComponent<ObjectSpawn>();
            areaPlaced.PlaceObject(currentObject);
            areaPlaced.RemoveHighlight();
        }

    }

    void FlipObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider.tag == "Object")
        {
            PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
            placedObject.ObjectFlip();
        }
    }

    void RemoveObject()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Block" || hit.collider.tag == "Object")
            {
                PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
                ObjectSpawn areaPlaced = placedObject.spawnArea;

                areaPlaced.RemoveObject();
            }
        }
    }

    public void ShowHighlights()
    {
        if (currentObject.tag == "Object")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightObjectPlacement();
            }
        }
        else if (currentObject.tag == "Block")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightBlockPlacement();
            }
        }
    }

    public void HideHighlights()
    {
        foreach (PlacementArea area in placementAreas)
        {
            area.RemoveHighlight();
        }
    }
}
