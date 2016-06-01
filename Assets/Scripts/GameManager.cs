using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject block;

    public float range = 100f;

    private bool inPlay = false;
    private GameObject currentObject;
    private GameObject startingBlock;
    private Vector3 startBlockStartPos;
    private Quaternion startBlockStartRot;

    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this);

        startingBlock = GameObject.FindGameObjectWithTag("StartBlock");
    }

    void Start ()
    {
        currentObject = block;
        startBlockStartPos = startingBlock.transform.position;
        startBlockStartRot = startingBlock.transform.rotation;
	}

    void Update ()
    {
        if (inPlay == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // place an object
                Debug.Log("Place Object Fire");
                PlaceObject();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                RemoveObject();
            }
        }
    }

    void PlaceObject()
    {
        // First Shoot a ray
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check if player is placing object in the right area
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider.tag == "ObjectPlacement")
        {
            PlacementArea areaPlaced = hit.collider.gameObject.GetComponent<PlacementArea>();

            // If placing a block check to see if full
            if (!areaPlaced.IsFull && currentObject.tag == "Block")
            {
                // Place Block
                Debug.Log(hit.point.x + " " + hit.point.y);
                Vector3 position = new Vector3(hit.point.x, hit.point.y, 0f);
                GameObject placedObject = Instantiate(currentObject, position, Quaternion.identity) as GameObject;

                // Assign Object to area
                PlaceableObject obj = placedObject.GetComponent<PlaceableObject>();
                obj.areaPlaced = areaPlaced;

                // Update objects placed in area
                areaPlaced.PlaceBlock();
                Debug.Log("Placed Block");
            }

            // If placing a Object check to see if anything else has been placed
            if ((!areaPlaced.IsFull || areaPlaced.BlocksPlaced) && currentObject.tag == "Object")
            {
                // Place Block
                Debug.Log(hit.point.x + " " + hit.point.y);
                Vector3 position = new Vector3(hit.point.x, hit.point.y, 0f);
                GameObject placedObject = Instantiate(currentObject, position, Quaternion.identity) as GameObject;

                // Assign Object to area
                PlaceableObject obj = placedObject.GetComponent<PlaceableObject>();
                obj.areaPlaced = areaPlaced;

                // Update objects placed in area
                areaPlaced.PlaceObject();
                Debug.Log("Placed Object");
            }
        }
    }

    void RemoveObject()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
        PlacementArea areaPlaced = placedObject.areaPlaced;

        if (hit.collider.tag == "Block" )
        {
            areaPlaced.RemoveBlock();
            Destroy(hit.collider.gameObject);            
            Debug.Log(areaPlaced);
        }

        else if (hit.collider.tag == "Object")
        {
            //PlacementArea areaPlaced = hit.collider.gameObject.GetComponent<PlacementArea>();
            areaPlaced.RemoveObject();
            Destroy(hit.collider.gameObject);
        }
    }

    public void ChangeCurrentObject(GameObject newObject)
    {
        currentObject = newObject;
    }

    public void Restart()
    {
        if (inPlay == true)
        {
            inPlay = false;
            GameEventManager.TriggerGameReset();
        }
    }

    public void ClearAll()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < blocks.Length; i++)
        {
            Destroy(blocks[i]);
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
    }

    public void Launch()
    {
        if (inPlay == false)
        {
            inPlay = true;
            GameEventManager.TriggerGameLaunch();
        }
    }

}
