using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    [Header("Audio")]
    public AudioClip[] blockContact;
    static int blockContacti = -1;
    AudioSource this_blockAudio;
    public AudioClip blockSet;
    bool hitOnce = false;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D body;
    private AudioSource blockContact;
    private bool collided = false;
    private int directionHit = 1;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        blockContact = GetComponent<AudioSource>();
        this_blockAudio = GetComponent<AudioSource>();

    }

    public void OnCollisionEnter2D ( Collision2D other )
        {
        ///Dont care if any collision occurs while placing objects
        if (!GameManager.instance.inPlay || other.collider.tag=="Untagged")
            return;

        Debug.Log("Hit by" + other.gameObject.tag);
        if (other.transform.position.x < transform.position.x)   //force from left
                {
                directionHit = -1;
                }
            else
                {
                directionHit = 1;
                }
        if (!hitOnce && other.collider.tag == "Block" || other.collider.tag == "Object" || other.collider.tag == "StartBlock")
            {
            blockContacti = blockContacti + 1 >= 7 ? 0 : blockContacti + 1;
            this_blockAudio.clip = blockContact[blockContacti];
            this_blockAudio.Play();
            }
            body.AddTorque(25 * directionHit);
        hitOnce = true;
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
        hitOnce = false;
        blockContacti = -1;
        body.isKinematic = true;
        transform.rotation = initialRotation;
        transform.position = initialPosition;
        body.isKinematic = false;
        collided = false;
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

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block" && collided == false)
        {
            blockContact.Play();
            collided = true;
        }
    }
}
