using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public GameObject catapultBall;
    public Vector2 ballForce;
    public bool hasFired = false;

    private Vector3 ballPosition;
    private Rigidbody2D ballBody;

    void Awake()
    {
        ballBody = catapultBall.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    public void LaunchBall()
    {
        if (hasFired == false)
        {
            ballBody.AddForce(ballForce);
            hasFired = true;
        }
    }

    public void GameLaunch()
    {
        hasFired = false;
        ballPosition = catapultBall.transform.position;
    }

    public void GameReset()
    {
        ballBody.isKinematic = true;
        catapultBall.transform.position = ballPosition;
        ballBody.isKinematic = false;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

}
