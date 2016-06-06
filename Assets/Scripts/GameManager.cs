using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject block;
    GameObject winMessage, continueButton;

    public float range = 100f;

    public bool inPlay = false;
    private int placedObjects = 0;
    private GameObject currentObject, launchBotton;
    private int placeableMask;
    private Ray ray;
    private RaycastHit rayHit;
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
        DontDestroyOnLoad(gameObject);

        placeableMask = LayerMask.GetMask("Placeable");
        startingBlock = GameObject.FindGameObjectWithTag("StartBlock");

        winMessage = GameObject.Find("Canvas").transform.Find("Win Message").gameObject;
        continueButton = GameObject.Find("Canvas").transform.Find("ContinueButton").gameObject;
        }

    void Start ()
    {
        launchBotton = GameObject.Find("Canvas").transform.Find("Launch Button").gameObject;
        currentObject = block;
        startBlockStartPos = startingBlock.transform.position;
        startBlockStartRot = startingBlock.transform.rotation;
        winMessage.SetActive(false);
        continueButton.SetActive(false);
	}

    void Update ()
    {
        if (!inPlay)
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
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        ///When there is nothin in the area that I clicked
        if (!hit)
            return;

        ///If there is any other object in the area, I do not place anything
        Collider2D[] hits =  Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),0.5f);
        foreach (Collider2D i in hits)
            {
            if (i.gameObject.tag == "Block" || i.gameObject.tag == "Object" || i.gameObject.tag == "StartBlock")
                return;
            }

        Debug.Log("Raycast hit: " + hit.collider.gameObject.tag);
        if (hit.collider.tag == "ObjectPlacement" && placedObjects < 20)
        {
            Debug.Log(hit.point.x + " " + hit.point.y);
            Vector3 position = new Vector3(hit.point.x, hit.point.y, 0f);
            Instantiate (currentObject, position, Quaternion.identity);
            placedObjects++;
        }
            
    }

    void RemoveObject()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        ///When there is nothin in the area that I clicked
        if (!hit)
            return;

        Debug.Log("Raycast hit: " + hit.collider.gameObject.tag);
        if (hit.collider.tag == "Block" || hit.collider.tag == "Object" || hit.collider.tag == "StartBlock")
        {
            placedObjects--;
            Destroy(hit.collider.gameObject);
        }
    }

    public void ChangeCurrentObject(GameObject newObject)
    {
        currentObject = newObject;
    }

    public void Restart()
    {
        if (inPlay)
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
        placedObjects = 0;
        inPlay = true;
        Launch();
    }

    public void Launch()
    {
        if (!inPlay)    //Switch to playing mode
            {
            inPlay = true;
            launchBotton.GetComponentInChildren<Text>().text = "Playing";
            GameEventManager.TriggerGameLaunch();
            }
        else if (inPlay)
            {
            winMessage.SetActive(false);
            continueButton.SetActive(false);
            inPlay = false;
            launchBotton.GetComponentInChildren<Text>().text = "Building";
            GameEventManager.TriggerGameReset();
            }
    }

}
