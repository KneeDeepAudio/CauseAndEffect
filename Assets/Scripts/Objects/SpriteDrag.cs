using UnityEngine;

public class SpriteDrag : MonoBehaviour
{
    public LayerMask layer;

    [HideInInspector]
    public bool is_beingDragged = false;

    private Vector3 startLocalPos;
    private ObjectSpawn spawnArea;
    private Vector3 startMousePos;
    private Vector3 startPos;
    private float offsetX, offsetY;

    void Update()
    {
        if (is_beingDragged)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(target.x + offsetX, target.y + offsetY, 0f);
        }
    }

    public void OnMouseDown()
    {
        GameManager.instance.dragging = true;

        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offsetX = transform.position.x - offset.x;
        offsetY = transform.position.y - offset.y;

        startLocalPos = transform.localPosition;
        spawnArea = gameObject.GetComponent<PlaceableObject>().spawnArea;
        SetLayerRecursively(LayerMask.NameToLayer("IgnoreCollision"));
    }

    public void OnMouseDrag()
    { 
        is_beingDragged = true;
        spawnArea.full = false;

        if (gameObject.tag == "Object")
        {
            InputManager.instance.ShowHighlights("Object");
        }
        else if(gameObject.tag == "CatapultBall")
        {
            InputManager.instance.ShowHighlights("Sphere");
        }
        else if (gameObject.tag == "Block")
        {
            InputManager.instance.ShowHighlights("Block");
        }
    }

    public void OnMouseUp()
    {
        
        is_beingDragged = false;
        SetLayerRecursively(LayerMask.NameToLayer("Object"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, layer);

        transform.localPosition = startLocalPos;
        if(hit)
            Debug.Log(hit.collider.gameObject.name);
        else
                Debug.Log("Hit Nothing");

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

        InputManager.instance.HideHighlights();
        GameManager.instance.dragging = false;
    }

    public void SetLayerRecursively(int layerNumber)
    {
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
