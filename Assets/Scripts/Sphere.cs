using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void GameReset()
    {
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;
        transform.rotation = initialRotation;
        transform.position = initialPosition;
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
