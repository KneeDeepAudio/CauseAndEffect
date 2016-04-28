using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D body;
    private AudioSource blockContact;
    private bool collided = false;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        blockContact = GetComponent<AudioSource>();
    }

    void Start()
    {
        initialPosition = GetComponent<Transform>().position;
        initialRotation = GetComponent<Transform>().rotation;
    }

    void GameReset()
    {
        body.isKinematic = true;
        transform.rotation = initialRotation;
        transform.position = initialPosition;
        body.isKinematic = false;
        collided = false;
    }

    void OnEnable()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
       // GameEventManager.GameLaunch -= GameLaunch;
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
