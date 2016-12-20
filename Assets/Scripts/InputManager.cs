using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager manager;

    void Awake()
    {
        manager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            PlaceObject();
        }
    }

    public void PlaceObject()
    {
        // First Shoot a ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // If nothing is hit, stop doing things
        if (!hit)
            return;

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
}
