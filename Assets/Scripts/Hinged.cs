using UnityEngine;
using System.Collections;

public class Hinged : MonoBehaviour {

    public static Hinged instance;
    public GameObject selectedHighlight/*,chain*/,connectedBlock;
    public bool selected = true;

    LineRenderer chain;

    Vector3 position;
    Vector3 chainDir;

    // Use this for initialization
    void Start ()
    {
        position = transform.position;
        selectedHighlight.SetActive(false);
        //chain.SetActive(false);
        if (instance == null)
            instance = this;

        chain = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (connectedBlock != null)
        {
            chain.enabled = true;
            chain.SetPosition(0,transform.position);
            chain.SetPosition(1,connectedBlock.transform.position);
        }
        else
        {
            chain.enabled = false;
            chain.SetPosition(1,transform.position);
        }
    }

    void OnMouseDown()
    {
        selected = selected == true ? false : true;
        if (selected)
        {
            Debug.Log("Hinge Seleced");
            selectedHighlight.SetActive(true);
        }
        else
            selectedHighlight.SetActive(false);
    }

    public void Chain ( GameObject block )
    {
        //chain.transform.SetParent(block.transform);
        //chain.transform.position = new Vector3(0,0,0);
        //chain.SetActive(true);
        if (connectedBlock != null)
        {
            Destroy(connectedBlock.GetComponent<DistanceJoint2D>());
        }
        connectedBlock = block;
        chainDir = block.transform.position - transform.position;
        Debug.Log("Chained" + chainDir.magnitude);
        selectedHighlight.SetActive(false);
        // chainDir.Normalize();
        //chain.transform.rotation = Quaternion.identity;
        //chain.transform.rotation = Quaternion.Euler(chainDir);
        //chain.transform.localScale = new Vector3(1,chainDir.magnitude*0.5f,1);
        //chain.transform.position = new Vector3(0,0,0);
    }

    public void GameLaunch()
    {

    }

    public void GameReset()
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }

    void OnEnable ()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable ()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }
}
