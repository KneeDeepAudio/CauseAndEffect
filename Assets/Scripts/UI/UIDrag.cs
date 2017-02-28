using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour {

    public GameObject prefab;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    public void BeginDrag()
    {
        GameManager.instance.dragging = true;

        if(prefab.tag == "Object")  
        {
            InputManager.instance.ShowHighlights("Object");
        }
        else if(prefab.tag == "CatapultBall")
        {
            InputManager.instance.ShowHighlights("Sphere");
        }
        else if (prefab.tag == "Block")
        {
            InputManager.instance.ShowHighlights("Block");
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

        InputManager.instance.HideHighlights();
        GameManager.instance.dragging = false;

    }
}
