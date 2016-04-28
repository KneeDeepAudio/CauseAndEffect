﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject block;

    public float range = 100f;

    private bool inPlay = false;
    private Button launchBotton;
    private GameObject currentObject;
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
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider.tag == "ObjectPlacement")
        {
            Debug.Log(hit.point.x + " " + hit.point.y);
            Vector3 position = new Vector3(hit.point.x, hit.point.y, 0f);
            Instantiate (currentObject, position, Quaternion.identity);
        }
            
    }

    void RemoveObject()
    {
        Debug.Log("Raycast Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider.tag == "Block" || hit.collider.tag == "Object")
        {
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
