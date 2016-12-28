using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public bool inPlay = false;
    public bool dragging = false;

#if old_way
    //public AudioClip[] chooseObject;
    //int chooseObjecti = 0;
    //public GameObject block;
    //public float range = 100f;
    //public float maxObjectDistance = 0.5f;    

    //private AudioSource canvasSource;    
    //private int placeableMask;
    //private Ray ray;
    //private RaycastHit rayHit;
    //private GameObject startingBlock;
    //private Vector3 startBlockStartPos;
    //private Quaternion startBlockStartRot;
#endif

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
    }

#if old_way
    void Start()
    {
        canvasSource = GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>();
        startBlockStartPos = startingBlock.transform.position;
        startBlockStartRot = startingBlock.transform.rotation;
    }


    void Update()
    {
        //    if (inPlay == false)
        //    {
        //        if (Input.GetButtonDown("Fire1"))
        //        {
        //            // place an object
        //            Debug.Log("Place Object Fire");
        //            //PlaceObject();
        //        }
        //    )

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
/*
        // Check if player is placing object in the right area
        if (hit.collider.tag == "ObjectPlacement")
        {

            //If there is any other object in the area, place nothing
            Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), maxObjectDistance);
            foreach (Collider2D i in hits)
            {
                if (i.gameObject.tag == "Block" || i.gameObject.tag == "Object" || i.gameObject.tag == "StartBlock")
                    return;
            }

            //Grab area
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
            if ((!areaPlaced.ObjectPlaced && !areaPlaced.BlocksPlaced) && currentObject.tag == "Object")
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
*/
        if(hit.collider.tag == "Object")
        {
            PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
            placedObject.ObjectFlip();
        }
    }

    void RemoveObject ()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin,ray.direction);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Block" || hit.collider.tag == "Object")
            {
                PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
                ObjectSpawn areaPlaced = placedObject.spawnArea;

                areaPlaced.RemoveObject();
            }
        }

        /* As done with old placement using placement area
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Block")
            {
                PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
                PlacementArea areaPlaced = placedObject.areaPlaced;

                areaPlaced.RemoveBlock();
                Destroy(hit.collider.gameObject);
                Debug.Log(areaPlaced);
                }
            else if (hit.collider.tag == "Object")
            {
                PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
                PlacementArea areaPlaced = placedObject.areaPlaced;

                //PlacementArea areaPlaced = hit.collider.gameObject.GetComponent<PlacementArea>();
                areaPlaced.RemoveObject();
                Destroy(hit.collider.gameObject);
            }
        }
        */
    }

#endif

    public void GameReset()
    {
        if (inPlay)
        {
            inPlay = false;
            GameEventManager.TriggerGameReset();
        }
    }

    public void GameLaunch()
    {
        if (!inPlay)
        {
            inPlay = true;
            GameEventManager.TriggerGameLaunch();
        }
    }

}
