using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public GameObject catapultBall;
    public Vector2 ballForce;
    public bool hasFired = false;

    private Vector3 ballPosition;
    private Rigidbody2D ballBody;
    private AudioSource catapultLaunch;

    void Awake()
    {
        ballBody = catapultBall.GetComponent<Rigidbody2D>();
        catapultLaunch = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        ballPosition = catapultBall.transform.position;
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    public void LaunchBall()
    {
        if (hasFired == false)
        {
            catapultLaunch.Play();
            ballBody.AddForce(ballForce);
            hasFired = true;
            catapultBall.GetComponent<CatapultBall>().Travel();
        }
    }

    public void GameLaunch()
    {
        hasFired = false;
        
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
