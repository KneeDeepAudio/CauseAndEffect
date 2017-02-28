using UnityEngine;
using System.Collections;

public class BlockNew : MonoBehaviour {

    [Header("Audio")]
    public AudioClip[] blockContact;
    AudioSource this_blockAudio;
    public AudioClip blockSet;
    bool hitOnce = false;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D body;

    DistanceJoint2D dJoint;
    float dJointDistance;

//    private int directionHit = 1;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
       // blockContact = GetComponent<AudioSource>();
        this_blockAudio = GetComponent<AudioSource>();

    }

    public void OnCollisionEnter2D ( Collision2D other )
        {
        ///Dont care if any collision occurs while placing objects
        if (!GameManager.instance.inPlay || other.collider.tag=="Untagged")
            return;

        if (dJoint != null)  //Enable DJoint and start pulling
            {
            Debug.Log("Pulling");
            dJoint.enabled = true;
            StartCoroutine(ShortenChain());
            }

        Debug.Log("Hit by" + other.gameObject.tag);

        // Unkown purpose for this block of code
        /*
        if (other.transform.position.x < transform.position.x)   //force from left
                {
                directionHit = -1;
                }
            else
                {
                directionHit = 1;
                }
        */

        if (!hitOnce && other.collider.tag == "Block" || other.collider.tag == "Object" || other.collider.tag == "StartBlock")
            {
           // blockContacti = blockContacti + 1 >= 7 ? 0 : blockContacti + 1;
            this_blockAudio.clip = blockContact[0];
            this_blockAudio.Play();
            }

        hitOnce = true;
        }

    IEnumerator ShortenChain ()
        {
        Debug.Log(dJoint.distance > dJointDistance / 3);
        while (GameManager.instance.inPlay && dJoint.distance>dJointDistance/3)
            {
          //  Debug.Log("Shortening" + shortening);
            dJoint.distance = Mathf.Lerp(dJoint.distance,dJointDistance/3,Time.deltaTime);
            if (dJoint.distance-0.1f < dJointDistance / 3)
                break;
            yield return new WaitForEndOfFrame();
            }
        }

    void OnMouseDown ()
        {
        if (!Hinged.instance.selected)
            return;

        Hinged.instance.Chain(gameObject);
        gameObject.AddComponent<DistanceJoint2D>().enabled = false;
        dJoint = gameObject.GetComponent<DistanceJoint2D>();
        if (dJoint != null)
            {
            dJoint.enabled = false;
            dJoint.connectedBody = /*GameObject.Find("StartingBlock")*/Hinged.instance.gameObject.GetComponent<Rigidbody2D>();
            dJointDistance = dJoint.distance;
            Debug.Log("Block is connected to: " + dJoint.connectedBody);
            }
        }

    void Start()
    {
        this_blockAudio.clip = blockSet;
        this_blockAudio.Play();
        initialPosition = GetComponent<Transform>().position;
        initialRotation = GetComponent<Transform>().rotation;
    }

    void GameReset()
    {
        if (dJoint != null)
            {
            dJoint.enabled = false;
            dJoint.distance = dJointDistance;
            }
        hitOnce = false;
        body.isKinematic = true;
        transform.rotation = initialRotation;
        transform.position = initialPosition;
        body.isKinematic = false;
    }

    void OnEnable()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        
        Debug.Log("Calling Enabledelegate");
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        // GameEventManager.GameLaunch -= GameLaunch;
        
        Debug.Log("Calling disabledelegate");
        GameEventManager.GameReset -= GameReset;
    }

  
}
