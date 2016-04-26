using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
}
