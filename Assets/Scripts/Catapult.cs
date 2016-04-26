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
        ballPosition = catapultBall.transform.position;
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    public void LaunchBall()
    {
        hasFired = true;
        ballBody.AddForce(ballForce);
    }

    public void GameLaunch()
    {
        hasFired = false;
    }

    public void GameReset()
    {
        ballBody.isKinematic = false;
        catapultBall.transform.position = ballPosition;
        ballBody.isKinematic = true;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

}
