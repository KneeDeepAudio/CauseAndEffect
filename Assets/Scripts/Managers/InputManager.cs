using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public GameObject currentObject;

    private PlacementArea[] placementAreas;
    private float pressTime;

    void Awake()
    {
        #region singleton
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this);
        #endregion
    }

    void Start()
    {
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            pressTime = Time.time;

            // First Shoot a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // If nothing is hit, stop doing things
            if (!hit)
                return;

            if (hit.collider.tag == "ObjectPlacement")
                PlaceObject(hit);
        }
        //if (Input.GetButtonDown("Fire2"))
        //    FlipObject();

        if (Input.GetKeyDown(KeyCode.Escape))
            //Pause();
            ;
    }

    public void PlaceObject(RaycastHit2D hit)
    {
        /*
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
        */

        //Grab area
        ObjectSpawn areaPlaced = hit.collider.gameObject.GetComponent<ObjectSpawn>();
        areaPlaced.PlaceObject(currentObject);
        areaPlaced.RemoveHighlight();

    }

    //void FlipObject(RaycastHit2D hit)
    //{
    //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

    //    //if (!hit)
    //    //    return;

    //    //if (hit.collider.tag == "Object")
    //    //{
    //    //    PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
    //    //    placedObject.ObjectFlip();
    //    //}

    //    PlaceableObject placedObject = hit.collider.gameObject.GetComponent<PlaceableObject>();
    //    placedObject.ObjectFlip();
    //}

    void DragObject(RaycastHit2D hit)
    {
        SpriteDrag dragObject = hit.collider.gameObject.GetComponent<SpriteDrag>();
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

    public void ShowHighlights(string tag)
    {
        HideHighlights();
        if (tag == "Object")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightObjectPlacement();
            }
        }
        else if (tag == "Sphere")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightSpherePlacement();
            }
        }
        else if (tag == "Block")
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

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
    }

    private void OnEnable()
    {
        GameEventManager.GameLaunch += HideHighlights;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        GameEventManager.GameLaunch -= HideHighlights;
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

}
