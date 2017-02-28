using UnityEngine;
using System.Collections;

public class StartingBlock : MonoBehaviour {

    public float force = 25;
    public bool startRight = false;

    public Rigidbody2D rBody;

    private float directionModifier;
    //private Vector3 startPosition;
    //private Quaternion startRotation;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    //void Start()
    //{
    //    startRotation = transform.rotation;
    //    startPosition = transform.position;
    //}

    //void GameReset()
    //{
    //    rBody.velocity = Vector2.zero;
    //    rBody.angularVelocity = 0f;
    //    transform.rotation = startRotation;
    //    transform.position = startPosition;
    //}

    void GameLaunch()
    {
        if (startRight == true)
        { 
            directionModifier = -1;
        }
        else
        {
            directionModifier = 1;
        }
        rBody.AddTorque(force * directionModifier);
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        //GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameLaunch -= GameLaunch;
        //GameEventManager.GameReset -= GameReset;
    }

    public void AddTorque(float torque, ForceMode2D mode = ForceMode2D.Force) { }

}
