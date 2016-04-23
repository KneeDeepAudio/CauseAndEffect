using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject block;
    public GameObject trampoline;
    public GameObject catapault;
    public GameObject balista;

    public float range = 100f;

    private Button launchBotton;
    private GameObject currentObject;
    private int placeableMask;
    private Ray ray;
    private RaycastHit rayHit;

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
        DontDestroyOnLoad(gameObject);

        placeableMask = LayerMask.GetMask("Placeable");
    }

    void Start ()
    {
        currentObject = block;   
	}

    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // place an object
            Debug.Log("Place Object Fire");
            PlaceObject();
        }
    }

    void PlaceObject()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider.tag == "ObjectPlacement")
        {
            Vector3 position = new Vector3(hit.point.x, hit.point.y, 0f);
            Instantiate (currentObject, position, Quaternion.identity);
        }
            
    }

    public void ChangeCurrentObject(GameObject newObject)
    {
        currentObject = newObject;
    }
}
